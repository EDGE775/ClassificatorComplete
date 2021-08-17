using KPLN_Loader;
using System;
using System.Collections.Generic;
using static KPLN_Loader.Output.Output;

namespace ClassificatorComplete
{
    public class KplnOutput : Output
    {
        private Dictionary<OutputMessageType, Preferences.MessageType> typesMap
            = new Dictionary<OutputMessageType, Preferences.MessageType>();

        public KplnOutput()
        {
            typesMap.Add(OutputMessageType.Code, Preferences.MessageType.Code);
            typesMap.Add(OutputMessageType.Critical, Preferences.MessageType.Critical);
            typesMap.Add(OutputMessageType.Error, Preferences.MessageType.Error);
            typesMap.Add(OutputMessageType.Header, Preferences.MessageType.Header);
            typesMap.Add(OutputMessageType.Regular, Preferences.MessageType.Regular);
            typesMap.Add(OutputMessageType.Success, Preferences.MessageType.Success);
            typesMap.Add(OutputMessageType.System_OK, Preferences.MessageType.System_OK);
            typesMap.Add(OutputMessageType.System_Regular, Preferences.MessageType.System_Regular);
            typesMap.Add(OutputMessageType.Warning, Preferences.MessageType.Warning);
        }

        public override void PrintInfo(string value, OutputMessageType type)
        {
            Print(value, typesMap[type]);
        }

        public override void PrintDebug(string value, OutputMessageType type, bool check)
        {
            if (check)
            {
                PrintInfo(value, type);
            }
        }

        public override void PrintErr(Exception e, string value)
        {
            PrintError(e, value);
        }
    }
}
