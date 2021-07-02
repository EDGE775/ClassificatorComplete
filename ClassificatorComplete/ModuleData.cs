using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassificatorComplete
{
    public static class ModuleData
    {
        public static bool isDocumentAvailable { get; set; } = true;
#if Revit2020
        public static string RevitVersion = "2020";
        public static Window RevitWindow { get; set; }
#endif
#if Revit2018
        public static string RevitVersion = "2018";
#endif
        public static System.IntPtr MainWindowHandle { get; set; } //Главное окно Revit (WPF: Для определения свойства .Owner)
    }
}
