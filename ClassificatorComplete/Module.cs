using Autodesk.Revit.UI;
using KPLN_Loader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Module : IExternalModule
    {
        public static string assemblyPath = "";
        public Result Execute(UIControlledApplication application, string tabName)
        {
            assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            try { application.CreateRibbonTab(tabName); } catch { }

            string panelName = "Классификатор";
            RibbonPanel panel = null;
            List<RibbonPanel> tryPanels = application.GetRibbonPanels(tabName).Where(i => i.Name == panelName).ToList();
            if (tryPanels.Count == 0)
            {
                panel = application.CreateRibbonPanel(tabName, panelName);
            }
            else
            {
                panel = tryPanels.First();
            }

            PushButton btnHostMark = panel.AddItem(new PushButtonData(
                "ClassificatorCompleteCommand",
                "ClassificatorComplete",
                assemblyPath,
                "ClassificatorComplete.Command")
                ) as PushButton;


            return Result.Succeeded;
        }

        public Result Close()
        {
            return Result.Succeeded;
        }
    }
}
