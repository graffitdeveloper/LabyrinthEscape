using System.Xml.Serialization;

namespace LabyrinthEscape.HighScoreControls
{
    /// <summary>
    /// Класс лидера
    /// </summary>
    public class HighScoreItem
    {
        /// <summary>
        /// Конструктор по умолчанию (необходим для xml)
        /// </summary>
        public HighScoreItem()
        {

        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="name">Имя игрока</param>
        /// <param name="time">Время, за которое игрок прошел лабиринт</param>
        /// <param name="doneSteps">Количество шагов, которое сделал игрок</param>
        public HighScoreItem(string name, int time, int doneSteps)
        {
            Name = name;
            Time = time;
            DoneSteps = doneSteps;
        }

        /// <summary>
        /// Имя игрока
        /// </summary>
        [XmlAttribute("Name")] public string Name;

        /// <summary>
        /// Время, за которое игрок прошел лабиринт
        /// </summary>
        [XmlAttribute("Time")] public int Time;

        /// <summary>
        /// Количество шагов, которое сделал игрок
        /// </summary>
        [XmlAttribute("DoneSteps")] public int DoneSteps;
    }
}