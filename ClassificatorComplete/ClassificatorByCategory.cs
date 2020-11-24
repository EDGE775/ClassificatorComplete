using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace ClassificatorComplete
{
    [Serializable]
    public class ClassificatorByCategory
    {
        public string ClassificatorName;
        public BuiltInCategory BuiltInName;
        public string FamilyName;
        public string TypeName;

        public ClassificatorByCategory()
        {

        }
    }
}
