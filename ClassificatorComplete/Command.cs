using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.IO;
using System.Xml;
using static KPLN_Loader.Output.Output;
using Autodesk.Revit.UI.Selection;

namespace ClassificatorComplete
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Command : IExternalCommand
    {
        public static void PrintDebug(string value, KPLN_Loader.Preferences.MessageType type, bool check)
        {
            if (check)
            {
                Print(value, type);
            }
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            Document doc = commandData.Application.ActiveUIDocument.Document;
            Selection sel = commandData.Application.ActiveUIDocument.Selection;
            View activeView = doc.ActiveView;

            StorageUtils utils = new StorageUtils();
            InfosStorage storage = utils.getStorage();
            if (storage == null)
            {
                Print("Не удалось обработать конфигурационный файл!", KPLN_Loader.Preferences.MessageType.Error);
                return Result.Cancelled;
            }

            //Получаем список категорий для заполнения классификатора по типу:
            List<BuiltInCategory> constrCats = storage.constrCats;
            //Получаем список категорий для заполнения классификатора по экземпляру:
            List<BuiltInCategory> constrCatsByInstanse = storage.constrCatsByInstanse;

            int typeCounter = 0;
            int instanceCounter = 0;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Заполнение классификаторов");

                if (!constrCats[0].Equals(BuiltInCategory.INVALID))
                {
                    List<Element> constrsTypes = new FilteredElementCollector(doc).WhereElementIsElementType()
                        .WherePasses(new ElementMulticategoryFilter(constrCats))
                        .ToElements()
                        .Where(i => i.get_Parameter(BuiltInParameter.UNIFORMAT_CODE) != null)
                        .ToList();

                    if (constrsTypes == null || constrsTypes.Count == 0)
                    {
                        Print("Не удалось получить элементы для заполнения классиифкатора по типу!", KPLN_Loader.Preferences.MessageType.Error);
                        return Result.Cancelled;
                    }

                    foreach (ClassificatorByCategory classificator in storage.classificatorByCategory)
                    {
                        PrintDebug(string.Format("{0} - {1} - {2}", classificator.FamilyName, classificator.TypeName, classificator.ClassificatorName), KPLN_Loader.Preferences.MessageType.Code, storage.debugMode);
                    }
                    PrintDebug("Заполнение классификатора по типу ↑", KPLN_Loader.Preferences.MessageType.Header, storage.debugMode);
                    foreach (ElementType elem in constrsTypes)
                    {
                        PrintDebug(string.Format("{0}:{1}", elem.Name, elem.FamilyName), KPLN_Loader.Preferences.MessageType.System_Regular, storage.debugMode);
                        foreach (ClassificatorByCategory classificator in storage.classificatorByCategory)
                        {
                            bool categoryCatch = Category.GetCategory(doc, classificator.BuiltInName).Id.IntegerValue == elem.Category.Id.IntegerValue;
                            bool familyNameCatch = ParamUtils.nameChecker(classificator.FamilyName, elem.FamilyName);
                            bool typeNameCatch = ParamUtils.nameChecker(classificator.TypeName, elem.Name);
                            bool familyNameNotExist = classificator.FamilyName.Length == 0;
                            bool typeNameNotExist = classificator.TypeName.Length == 0;

                            if (categoryCatch && familyNameCatch && typeNameCatch && !familyNameNotExist && !typeNameNotExist)
                            {
                                //PrintDebug("1", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                ParamUtils.setBuiltInParam(elem, BuiltInParameter.UNIFORMAT_CODE, classificator.ClassificatorName);
                                PrintDebug(string.Format("Был присвоен код: {0}", classificator.ClassificatorName), KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                typeCounter++;
                                break;
                            }

                            if (categoryCatch && familyNameCatch && !familyNameNotExist && typeNameNotExist)
                            {
                                //PrintDebug("2", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                ParamUtils.setBuiltInParam(elem, BuiltInParameter.UNIFORMAT_CODE, classificator.ClassificatorName);
                                PrintDebug(string.Format("Был присвоен код: {0}", classificator.ClassificatorName), KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                typeCounter++;
                                break;
                            }

                            if (categoryCatch && typeNameCatch && familyNameNotExist && !typeNameNotExist)
                            {
                                //PrintDebug("3", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                ParamUtils.setBuiltInParam(elem, BuiltInParameter.UNIFORMAT_CODE, classificator.ClassificatorName);
                                PrintDebug(string.Format("Был присвоен код: {0}", classificator.ClassificatorName), KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                typeCounter++;
                                break;
                            }

                            if (categoryCatch && familyNameNotExist && typeNameNotExist)
                            {
                                //PrintDebug("4", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                ParamUtils.setBuiltInParam(elem, BuiltInParameter.UNIFORMAT_CODE, classificator.ClassificatorName);
                                PrintDebug(string.Format("Был присвоен код: {0}", classificator.ClassificatorName), KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                typeCounter++;
                                break;
                            }
                        }
                    }
                }

                if (!constrCatsByInstanse[0].Equals(BuiltInCategory.INVALID))
                {
                    List<Element> constrsInstances;

                    if (sel.GetElementIds().Count == 0)
                    {
                        constrsInstances = new FilteredElementCollector(doc)
                            .WhereElementIsNotElementType()
                            .WherePasses(new ElementMulticategoryFilter(constrCatsByInstanse))
                            .ToList();
                    }
                    else
                    {
                        constrsInstances = sel.GetElementIds().Select(elem => doc.GetElement(elem)).ToList();
                    }

                    if (constrsInstances == null || constrsInstances.Count == 0)
                    {
                        Print("Не удалось получить элементы для заполнения классиифкатора по экземпляру!", KPLN_Loader.Preferences.MessageType.Error);
                        return Result.Cancelled;
                    }
                    foreach (ClassificatorByInstanse classificator in storage.classificatorByInstanse)
                    {
                        PrintDebug(string.Format("{0} - {1}", classificator.FamilyName, classificator.TypeName), KPLN_Loader.Preferences.MessageType.Code, storage.debugMode);
                    }
                    PrintDebug("Заполнение классификатора по экземпляру ↑", KPLN_Loader.Preferences.MessageType.Header, storage.debugMode);

                    ParamUtils utilsForClassificatorByInstanse = new ParamUtils();

                    foreach (Element elem in constrsInstances)
                    {
                        PrintDebug(string.Format("{0} : {1} : {2}", elem.Name, elem.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString(), elem.Id.IntegerValue), KPLN_Loader.Preferences.MessageType.System_Regular, storage.debugMode);
                        foreach (ClassificatorByInstanse classificator in storage.classificatorByInstanse)
                        {
                            bool categoryCatch = Category.GetCategory(doc, classificator.BuiltInName).Id.IntegerValue == elem.Category.Id.IntegerValue;
                            bool familyNameCatch = ParamUtils.nameChecker(classificator.FamilyName, elem.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString());
                            bool typeNameCatch = ParamUtils.nameChecker(classificator.TypeName, elem.Name);
                            bool familyNameNotExist = classificator.FamilyName.Length == 0;
                            bool typeNameNotExist = classificator.TypeName.Length == 0;

                            if (categoryCatch && familyNameCatch && typeNameCatch && !familyNameNotExist && !typeNameNotExist)
                            {
                                //PrintDebug("1", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                utilsForClassificatorByInstanse.setClassificator(classificator, storage, elem, storage.debugMode);
                                instanceCounter++;
                                break;
                            }
                            if (categoryCatch && familyNameCatch && !familyNameNotExist && typeNameNotExist)
                            {
                                //PrintDebug("2", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                utilsForClassificatorByInstanse.setClassificator(classificator, storage, elem, storage.debugMode);
                                instanceCounter++;
                                break;
                            }
                            if (categoryCatch && typeNameCatch && familyNameNotExist && !typeNameNotExist)
                            {
                                //PrintDebug("3", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                utilsForClassificatorByInstanse.setClassificator(classificator, storage, elem, storage.debugMode);
                                instanceCounter++;
                                break;
                            }
                            if (categoryCatch && familyNameNotExist && typeNameNotExist)
                            {
                                //PrintDebug("4", KPLN_Loader.Preferences.MessageType.System_OK, storage.debugMode);
                                utilsForClassificatorByInstanse.setClassificator(classificator, storage, elem, storage.debugMode);
                                instanceCounter++;
                                break;
                            }
                        }
                    }

                    if (activeView.Name.Contains("3D"))
                    {
                        ViewUtils viewUtils = new ViewUtils(doc);

                        foreach (Element elem in utilsForClassificatorByInstanse.fullSuccessElems)
                        {
                            if (elem is Group) continue;
                            try
                            {
                                activeView.SetElementOverrides(elem.Id, viewUtils.fullSuccessSet);
                            }
                            catch { }
                        }
                        foreach (Element elem in utilsForClassificatorByInstanse.notFullSuccessElems)
                        {
                            if (elem is Group) continue;
                            try
                            {
                                activeView.SetElementOverrides(elem.Id, viewUtils.notFullSuccessSet);
                            }
                            catch { }
                        }
                    }
                }
                t.Commit();
            }
            TaskDialog.Show("Отчёт", "Обработано элементов по типу: " + typeCounter + "\nОбработано элементов по экземпляру: " + instanceCounter);
            return Result.Succeeded;
        }
    }
}
