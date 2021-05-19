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
using ClassificatorComplete.Data;
using ClassificatorComplete.Forms;

namespace ClassificatorComplete
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class CommandStartClassificator : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            Selection sel = commandData.Application.ActiveUIDocument.Selection;
            View activeView = doc.ActiveView;

            StorageUtils utils = new StorageUtils();

            ClassificatorForm form = new ClassificatorForm(utils);
            form.ShowDialog();
            if (form.DialogResult != System.Windows.Forms.DialogResult.OK) return Result.Cancelled;

            bool debugMode = form.debugMode;

            InfosStorage storage = form.storage;
            if (storage == null)
            {
                Print("Не удалось обработать конфигурационный файл!", KPLN_Loader.Preferences.MessageType.Error);
                return Result.Cancelled;
            }

            List<BuiltInCategory> constrCats = form.checkedCats;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Заполнение параметров классификатора");

                if (form.instanceOrType == 2)
                {
                    List<Element> constrsTypes;

                    if (sel.GetElementIds().Count == 0)
                    {
                        constrsTypes = new FilteredElementCollector(doc).WhereElementIsElementType()
                            .WherePasses(new ElementMulticategoryFilter(constrCats))
                            .ToElements()
                            .Where(i => i.get_Parameter(BuiltInParameter.UNIFORMAT_CODE) != null)
                            .ToList();
                    }
                    else
                    {
                        constrsTypes = sel.GetElementIds()
                            .Select(elem => doc.GetElement(elem).GetTypeId())
                            .Select(type => doc.GetElement(type))
                            .ToHashSet()
                            .ToList();
                    }

                    ParamUtils utilsForType = new ParamUtils(debugMode);
                    utilsForType.startClassification(constrsTypes, storage, doc);
                    Print(string.Format("Обработано элементов: {0}", utilsForType.fullSuccessElems.Count + utilsForType.notFullSuccessElems.Count), KPLN_Loader.Preferences.MessageType.Success);
                }

                else if (form.instanceOrType == 1)
                {
                    List<Element> constrsInstances;

                    if (sel.GetElementIds().Count == 0)
                    {
                        constrsInstances = new FilteredElementCollector(doc)
                            .WhereElementIsNotElementType()
                            .WherePasses(new ElementMulticategoryFilter(constrCats))
                            .ToList();
                    }
                    else
                    {
                        constrsInstances = sel.GetElementIds().Select(elem => doc.GetElement(elem)).ToList();
                    }

                    ParamUtils utilsForInstanse = new ParamUtils(debugMode);
                    utilsForInstanse.startClassification(constrsInstances, storage, doc);

                    if (activeView.Name.Contains("3D"))
                    {
                        ViewUtils viewUtils = new ViewUtils(doc);

                        OverrideGraphicSettings overrideGraphic = ViewUtils.getStandartGraphicSettings(doc);
                        List<Element> elems = new FilteredElementCollector(doc, activeView.Id)
                             .WhereElementIsNotElementType()
                             .ToElements()
                             .ToList();

                        foreach (Element elem in elems)
                        {
                            if (elem is Group) continue;
                            try
                            {
                                activeView.SetElementOverrides(elem.Id, overrideGraphic);
                            }
                            catch { }
                        }
                        foreach (Element elem in utilsForInstanse.fullSuccessElems)
                        {
                            if (elem is Group) continue;
                            try
                            {
                                activeView.SetElementOverrides(elem.Id, viewUtils.fullSuccessSet);
                            }
                            catch { }
                        }
                        foreach (Element elem in utilsForInstanse.notFullSuccessElems)
                        {
                            if (elem is Group) continue;
                            try
                            {
                                activeView.SetElementOverrides(elem.Id, viewUtils.notFullSuccessSet);
                            }
                            catch { }
                        }
                    }
                    Print(string.Format("Обработано элементов: {0}", utilsForInstanse.fullSuccessElems.Count + utilsForInstanse.notFullSuccessElems.Count), KPLN_Loader.Preferences.MessageType.Success);
                }
                else
                {
                    Print("Выбрана некорректная операция! Проверьте конфигурационный файл.", KPLN_Loader.Preferences.MessageType.Success);
                }
                t.Commit();
            }
            return Result.Succeeded;
        }
    }
}
