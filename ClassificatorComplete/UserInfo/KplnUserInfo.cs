using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete.UserInfo
{
    public class KplnUserInfo : UserInfo
    {
        public string getUserName()
        {
            return string.Format("{0} {1}", KPLN_Loader.Preferences.User.Family, KPLN_Loader.Preferences.User.Name);
        }
    }
}
