using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace ClassificatorComplete
{
    [Serializable]
    public class InfosStorage
    {
        public List<BuiltInCategory> constrCats;
        public List<ClassificatorByCategory> classificatorByCategory;

        public string classCodeParam;
        public string classNameParam;
        public List<BuiltInCategory> constrCatsByInstanse;
        public List<ClassificatorByInstanse> classificatorByInstanse;

        public InfosStorage()
        {

        }
    }
}
