﻿using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.UI;


namespace ClassificatorComplete
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class App : IExternalApplication
    {
        public static string assemblyPath = "";

        public Result OnStartup(UIControlledApplication application)
        {
            assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            string tabName = "BIM-STARTER";
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

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}