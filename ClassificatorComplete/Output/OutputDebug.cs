using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KPLN_Loader.Output.Output;

namespace ClassificatorComplete
{
    public abstract class OutputDebug
    {
        public static void PrintDebug(string value, KPLN_Loader.Preferences.MessageType type, bool check)
        {
            if (check)
            {
                Print(value, type);
            }
        }
    }
}
