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

namespace ClassificatorComplete
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;

                string dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string folder = System.IO.Path.GetDirectoryName(dllPath);

                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.InitialDirectory = folder;
                dialog.Multiselect = false;
                dialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return Result.Cancelled;
                string xmlFilePath = dialog.FileName;

                InfosStorage storage = new InfosStorage();
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(InfosStorage));
                using (StreamReader r = new StreamReader(xmlFilePath))
                {
                    storage = (InfosStorage)serializer.Deserialize(r);
                }
                if (storage.LinkedFilesPrefix == null) storage.LinkedFilesPrefix = "#";

                //Получаем список конструкций для заполнения классификатора:
                List<BuiltInCategory> constrCats = storage.constrCats;

                List<Element> constrsTypes = new FilteredElementCollector(doc).WhereElementIsElementType()
                    .WherePasses(new ElementMulticategoryFilter(constrCats))
                    .ToElements()
                    .Where(i => i.get_Parameter(BuiltInParameter.UNIFORMAT_CODE) != null)
                    .ToList();

                if (constrsTypes == null || constrsTypes.Count == 0) throw new Exception("Не удалось получить элементы для заполнения!");
                int counter = 0;

                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Заполнение классификаторов");
                    foreach (ClassificatorByCategory classificator in storage.classificatorByCategory)
                    {
                        Print(string.Format("{0} - {1} - {2}", classificator.FamilyName, classificator.TypeName, classificator.ClassificatorName), KPLN_Loader.Preferences.MessageType.Code);
                    }
                    foreach (ElementType elem in constrsTypes)
                    {
                        Print(string.Format("{0}:{1}", elem.Name, elem.FamilyName), KPLN_Loader.Preferences.MessageType.System_Regular);
                        foreach (ClassificatorByCategory classificator in storage.classificatorByCategory)
                        {
                            bool categoryCatch = Category.GetCategory(doc, classificator.BuiltInName).Id.IntegerValue == elem.Category.Id.IntegerValue;
                            bool familyNameCatch = nameChecker(classificator.FamilyName, elem.FamilyName);
                            bool typeNameCatch = nameChecker(classificator.TypeName, elem.Name);
                            bool familyNameNotExist = classificator.FamilyName.Length == 0;
                            bool typeNameNotExist = classificator.TypeName.Length == 0;

                            if (categoryCatch && familyNameCatch && typeNameCatch && !familyNameNotExist && !typeNameNotExist)
                            {
                                Print("1", KPLN_Loader.Preferences.MessageType.System_OK);
                                elem.get_Parameter(BuiltInParameter.UNIFORMAT_CODE).Set(classificator.ClassificatorName);
                                counter++;
                                break;
                            }

                            if (categoryCatch && familyNameCatch && !familyNameNotExist && typeNameNotExist)
                            {
                                Print("2", KPLN_Loader.Preferences.MessageType.System_OK);
                                elem.get_Parameter(BuiltInParameter.UNIFORMAT_CODE).Set(classificator.ClassificatorName);
                                counter++;
                                break;
                            }

                            if (categoryCatch && typeNameCatch && familyNameNotExist && !typeNameNotExist)
                            {
                                Print("3", KPLN_Loader.Preferences.MessageType.System_OK);
                                elem.get_Parameter(BuiltInParameter.UNIFORMAT_CODE).Set(classificator.ClassificatorName);
                                counter++;
                                break;
                            }

                            if (categoryCatch && familyNameNotExist && typeNameNotExist)
                            {
                                Print("4", KPLN_Loader.Preferences.MessageType.System_OK);
                                elem.get_Parameter(BuiltInParameter.UNIFORMAT_CODE).Set(classificator.ClassificatorName);
                                counter++;
                                break;
                            }
                            //Print(string.Format("categoryCatch = {0} familyNameCatch = {1} typeNameCatch = {2} familyNameNotExist = {3} typeNameNotExist = {4}", categoryCatch.ToString(), familyNameCatch.ToString(),
                            //    typeNameCatch.ToString(), familyNameNotExist.ToString(), typeNameNotExist.ToString()), KPLN_Loader.Preferences.MessageType.Error);
                        }
                    }
                    t.Commit();
                }

                TaskDialog.Show("Отчёт", "Обработано элементов: " + counter);

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                PrintError(e);
                return Result.Failed;
            }
        }
        public static bool nameChecker(string nameClafi, string nameElem)
        {
            string[] arrayClafi = nameClafi.ToLower().Split(',');
            int index = arrayClafi.Length;

            if (index == 1)
            {
                if (nameElem.ToLower().Contains(arrayClafi.First()))
                {
                    return true;
                }
                return false;
            }
            else if (index > 1)
            {
                for (int i = 0; i < index; i++)
                {
                    if (nameElem.ToLower().Contains(arrayClafi[i])) continue;
                    else return false;
                }
                return true;
            }

            return false;
        }
    }
}
