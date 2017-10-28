using System.Xml.Serialization;

namespace LabyrinthEscape.HighScoreControls
{
    public class HighScoreItem
    {
        public HighScoreItem()
        {
            
        }

        public HighScoreItem(string name, int time)
        {
            Name = name;
            Time = time;
        }

        [XmlAttribute("Name")] public string Name;

        [XmlAttribute("Time")] public int Time;
    }
}