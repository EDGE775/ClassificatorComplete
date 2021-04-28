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
        public int instanceOrType;
        public List<string> instanseParams;
        public List<Classificator> classificator;

        public InfosStorage()
        {

        }
    }
}
