﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete.ExecutableCommand
{
    /// <summary>
    /// Интерфейс, определяющий среду для запуска команд Revit
    /// </summary>
    public interface CommandEnvironment
    {
        /// <summary>
        /// Метод передаёт объект реализующий интерфейс среды запуска в соответствующую очередь Queue для запуска команды в событии OnIdling
        /// </summary>
        /// <param name="obj"></param>
        void toEnqueue(object obj);
    }
}
