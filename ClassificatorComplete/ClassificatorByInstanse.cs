using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace ClassificatorComplete
{
    [Serializable]
    public class ClassificatorByInstanse
    {
        public List<string> paramsValues;
        public BuiltInCategory BuiltInName;
        public string FamilyName;
        public string TypeName;

        public ClassificatorByInstanse()
        {

        }
    }
}
