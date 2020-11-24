using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete
{
    [Serializable]
    public class InfosStorage
    {
        public string ConfigurationName;
        public string LinkedFilesPrefix;

        public List<ClassificatorByCategory> classificatorByCategory;

        public InfosStorage()
        {

        }
    }
}
