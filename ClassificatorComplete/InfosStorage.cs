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
        public bool debugMode;
        public List<BuiltInCategory> constrCats;
        public List<ClassificatorByCategory> classificatorByCategory;

        public List<string> instanseParams;
        public List<BuiltInCategory> constrCatsByInstanse;
        public List<ClassificatorByInstanse> classificatorByInstanse;

        public InfosStorage()
        {

        }
    }
}
