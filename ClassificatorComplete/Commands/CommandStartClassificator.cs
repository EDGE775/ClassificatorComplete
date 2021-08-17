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
using static ClassificatorComplete.ApplicationConfig;
using Autodesk.Revit.UI.Selection;
using ClassificatorComplete.Data;
using ClassificatorComplete.Forms;
using KPLN_Loader.Common;

namespace ClassificatorComplete
{
    class CommandStartClassificator : IExecutableCommand, MyExecutableCommand
    {
        private ClassificatorForm form { get; set; }

        public CommandStartClassificator(ClassificatorForm form)
        {
            this.form = form;
        }
        public Result Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            Selection sel = app.ActiveUIDocument.Selection;
            View activeView = doc.ActiveView;

            bool debugMode = form.debugMode;
            bool colourMode = form.colourMode;

            InfosStorage storage = form.storage;
            if (storage == null)
            {
                output.PrintInfo("Не удалось обработать конфигурационный файл!", Output.OutputMessageType.Error);
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
                    if (!utilsForType.startClassification(constrsTypes, storage, doc))
                    {
                        return Result.Cancelled;
                    }
                    output.PrintInfo(string.Format("Обработано элементов: {0}", utilsForType.fullSuccessElems.Count + utilsForType.notFullSuccessElems.Count), 
                        Output.OutputMessageType.Success);
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
                    if (!utilsForInstanse.startClassification(constrsInstances, storage, doc))
                    {
                        return Result.Cancelled;
                    }

                    if (colourMode && activeView.ViewType == ViewType.ThreeD)
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
                    output.PrintInfo(string.Format("Обработано элементов: {0}", 
                        utilsForInstanse.fullSuccessElems.Count + utilsForInstanse.notFullSuccessElems.Count), Output.OutputMessageType.Success);
                }
                else
                {
                    output.PrintInfo("Выбрана некорректная операция! Проверьте конфигурационный файл.", Output.OutputMessageType.Success);
                }

                try
                {
                    LastRunInfo.getInstanceOrCreateNew(doc).save(form.utils.xmlFilePath != null ? form.utils.xmlFilePath : "Файл конфигурации не был сохранён при последнем запуске!");
                }
                catch (Exception e)
                {
                    output.PrintErr(e, "Произошла ошибка в процессе сохранения информации о запуске плагина!");
                }

                t.Commit();
            }
            return Result.Succeeded;
        }
    }
}
