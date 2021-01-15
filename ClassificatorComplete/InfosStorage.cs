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
        public string ConfigurationName;
        public string LinkedFilesPrefix;

        public List<ClassificatorByCategory> classificatorByCategory;
        public List<BuiltInCategory> constrCats;

        public InfosStorage()
        {

        }
    }
}
