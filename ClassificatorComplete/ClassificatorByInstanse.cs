using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace ClassificatorComplete
{
    [Serializable]
    public class ClassificatorByInstanse
    {
        public string ClassificatorName;
        public string ClassificatorFullName;
        public BuiltInCategory BuiltInName;
        public string FamilyName;
        public string TypeName;

        public ClassificatorByInstanse()
        {

        }
    }
}
