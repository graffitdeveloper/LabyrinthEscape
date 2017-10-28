using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using LabyrinthEscape.GameManagerControls;

namespace LabyrinthEscape.HighScoreControls
{
    [XmlRoot("HighScoreDataCollection")]
    public class HighScoreData
    {
        [XmlArray("EasyHighScore")] [XmlArrayItem("HighScoreItem")]
        public List<HighScoreItem> EasyHighScore = new List<HighScoreItem>();

        [XmlArray("MediumHighScore")] [XmlArrayItem("HighScoreItem")]
        public List<HighScoreItem> MediumHighScore = new List<HighScoreItem>();

        [XmlArray("HardHighScore")] [XmlArrayItem("HighScoreItem")]
        public List<HighScoreItem> HardHighScore = new List<HighScoreItem>();

        private static string _path = "HighscoreData";

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(HighScoreData));
            var stream = new FileStream(_path, FileMode.Create);
            serializer.Serialize(stream, this);
            stream.Close();
        }

        public static HighScoreData Load()
        {
            var serializer = new XmlSerializer(typeof(HighScoreData));
            if (File.Exists(_path))
            {
                var stream = new FileStream(_path, FileMode.Open);
                var data = serializer.Deserialize(stream) as HighScoreData;
                stream.Close();
                return data;
            }

            return new HighScoreData();
        }

        public static void WriteNewResult(GameType gameType, string name, int time)
        {
            var data = Load();

            switch (gameType)
            {
                case GameType.Easy:
                    data.EasyHighScore = AddNewItemAndSortList(
                        data.EasyHighScore, new HighScoreItem(name, time));
                    break;

                case GameType.Medium:
                    data.MediumHighScore = AddNewItemAndSortList(
                        data.MediumHighScore, new HighScoreItem(name, time));
                    break;

                case GameType.Hard:
                    data.HardHighScore = AddNewItemAndSortList(
                        data.HardHighScore, new HighScoreItem(name, time));
                    break;

                default:
                    throw new ArgumentOutOfRangeException("gameType", gameType, null);
            }

            data.Save();
        }

        private static List<HighScoreItem> AddNewItemAndSortList(List<HighScoreItem> list, HighScoreItem newItem)
        {
            list.Add(newItem);
            var orderedList = list.OrderBy(item => item.Time).ToList();
            list.Clear();

            for (int i = 0; i < 10; i++)
            {
                if(i >= orderedList.Count) break;

                list.Add(orderedList[i]);
            }

            return list;
        }

        public static void ClearResults()
        {
            var data = new HighScoreData();
            data.Save();
        }
    }
}