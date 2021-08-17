using Autodesk.Revit.UI;

namespace ClassificatorComplete
{
    /// <summary>
    /// Интерфейс для запуска команд Revit (событие OnIdling запускает команду внутри текущего UIControlledApplication)
    /// </summary>
    public interface MyExecutableCommand
    {
        Result Execute(UIApplication app);
    }
}
