using System;

namespace LabyrinthEscape.SomeHelpers
{
    public class Helpers
    {
        public static string GetFormattedTimeFromSeconds(int seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}