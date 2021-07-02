﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassificatorComplete.Forms.ViewModels;
using KPLN_Loader.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete
{
    class CommandFindAllElementsInModel : IExecutableCommand
    {
        ObservableCollection<RuleItem> ruleItems;

        public CommandFindAllElementsInModel(ObservableCollection<RuleItem> ruleItems)
        {
            this.ruleItems = ruleItems;
        }

        public Result Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;

            foreach (var rule in ruleItems)
            {
                List<Element> elemsFromRule = CommandFindElementsInModel.getElemsFromModel(doc, rule);
                rule.elemsCountInModel = elemsFromRule.Count;
            }

            return Result.Succeeded;
        }
    }
}
