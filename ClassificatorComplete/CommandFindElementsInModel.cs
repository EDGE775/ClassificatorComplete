using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassificatorComplete.Forms.ViewModels;
using KPLN_Loader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete
{
    class CommandFindElementsInModel : IExecutableCommand
    {
        private RuleItem ruleItem;

        public CommandFindElementsInModel(RuleItem ruleItem)
        {
            this.ruleItem = ruleItem;
        }

        public Result Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            List<Element> constrs = getElemsFromModel(doc, ruleItem);
            Selection sel = app.ActiveUIDocument.Selection;
            sel.SetElementIds(constrs.Select(x => x.Id).ToList());
            ruleItem.elemsCountInModel = constrs.Count;
            return Result.Succeeded;
        }

        public static List<Element> getElemsFromModel(Document doc, RuleItem ruleItem)
        {
            try
            {
                List<BuiltInCategory> constrCats = new List<BuiltInCategory>() { ruleItem.builtInCategoryName };
                return new FilteredElementCollector(doc)
                    .WhereElementIsNotElementType()
                    .WherePasses(new ElementMulticategoryFilter(constrCats))
                    .Where(x => ParamUtils.nameChecker(ruleItem.familyNameValue, ParamUtils.getElemFamilyName(x)))
                    .Where(x => ParamUtils.nameChecker(ruleItem.typeNameValue, x.Name))
                    .ToList();
            }
            catch (Exception)
            {
            }
            return new List<Element>() { };
        }
    }
}
