using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassificatorComplete.Forms;
using System;
using static KPLN_Loader.Output.Output;

namespace ClassificatorComplete
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CommandOpenClassificatorForm : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                StorageUtils utils = new StorageUtils();
                ClassificatorForm form = new ClassificatorForm(utils);
                form.Show();
                //ParamSetter Form = new ParamSetter(commandData.Application.ActiveUIDocument.Document);
                //Form.Show();
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                PrintError(e);
                return Result.Failed;
            }
        }
    }
}
