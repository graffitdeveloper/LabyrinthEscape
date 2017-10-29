using System.Xml.Serialization;

namespace LabyrinthEscape.HighScoreControls
{
    public class HighScoreItem
    {
        public HighScoreItem()
        {
            
        }

        public HighScoreItem(string name, int time, int doneSteps)
        {
            Name = name;
            Time = time;
            DoneSteps = doneSteps;
        }

        [XmlAttribute("Name")] public string Name;

        [XmlAttribute("Time")] public int Time;

        [XmlAttribute("DoneSteps")] public int DoneSteps;
    }
}