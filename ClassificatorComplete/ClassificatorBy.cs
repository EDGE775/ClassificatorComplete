using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete
{
    public abstract class ClassificatorBy
    {
        public List<string> paramsValues;
        public BuiltInCategory BuiltInName;
        public string FamilyName;
        public string TypeName;
    }
}
