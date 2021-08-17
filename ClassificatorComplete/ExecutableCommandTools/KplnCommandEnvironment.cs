using KPLN_Loader.Common;

namespace ClassificatorComplete.ExecutableCommand
{
    public class KplnCommandEnvironment : CommandEnvironment
    {
        public void toEnqueue(object obj)
        {
            KPLN_Loader.Preferences.CommandQueue.Enqueue(obj as IExecutableCommand);
        }
    }
}
