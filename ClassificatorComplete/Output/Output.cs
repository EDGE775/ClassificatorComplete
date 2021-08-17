using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete
{
    public abstract class Output
    {
        public enum OutputMessageType { Header, Regular, Error, Warning, Critical, Code, Success, System_OK, System_Regular }

        public abstract void PrintDebug(string value, OutputMessageType type, bool check);
        public abstract void PrintInfo(string value, OutputMessageType type);
        public abstract void PrintErr(Exception e, string value);

    }
}
