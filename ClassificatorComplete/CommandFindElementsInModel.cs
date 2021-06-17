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
        private BuiltInCategory builtIn;
        private string familyName;
        private string typeName;

        public CommandFindElementsInModel(RuleItem ruleItem)
        {
            this.builtIn = ruleItem.builtInCategoryName;
            this.familyName = ruleItem.familyNameValue;
            this.typeName = ruleItem.typeNameValue;
        }

        public Result Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;

            List<BuiltInCategory> constrCats = new List<BuiltInCategory>() { builtIn };
            List<Element> constrs = new List<Element>();

            constrs = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .WherePasses(new ElementMulticategoryFilter(constrCats))
                .Where(x => ParamUtils.nameChecker(familyName, ParamUtils.getElemFamilyName(x)))
                .Where(x => ParamUtils.nameChecker(typeName, x.Name))
                .ToList();

            Selection sel = app.ActiveUIDocument.Selection;
            sel.SetElementIds(constrs.Select(x => x.Id).ToList());

            return Result.Succeeded;
        }
    }
}
