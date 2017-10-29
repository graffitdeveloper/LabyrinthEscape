using System;

namespace LabyrinthEscape.SomeHelpers
{
    /// <summary>
    /// Класс, в который можно добавлять общие вспомогательные методы
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Превращает время в секундах в строку типа ЧЧ:ММ:СС
        /// </summary>
        /// <param name="seconds">время в секундах, которое необходимо перевести в строку</param>
        public static string GetFormattedTimeFromSeconds(int seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}