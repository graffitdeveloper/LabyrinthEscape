using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using LabyrinthEscape.GameManagerControls;

namespace LabyrinthEscape.HighScoreControls
{
    /// <summary>
    /// Сериализующийся класс со списками лидеров
    /// </summary>
    [XmlRoot("HighScoreDataCollection")]
    public class HighScoreData
    {
        /// <summary>
        /// Список лидеров уровня Easy
        /// </summary>
        [XmlArray("EasyHighScore")] [XmlArrayItem("HighScoreItem")]
        public List<HighScoreItem> EasyHighScore = new List<HighScoreItem>();

        /// <summary>
        /// Список лидеров уровня Medium
        /// </summary>
        [XmlArray("MediumHighScore")] [XmlArrayItem("HighScoreItem")]
        public List<HighScoreItem> MediumHighScore = new List<HighScoreItem>();

        /// <summary>
        /// Список лидеров уровня Hard
        /// </summary>
        [XmlArray("HardHighScore")] [XmlArrayItem("HighScoreItem")]
        public List<HighScoreItem> HardHighScore = new List<HighScoreItem>();

        /// <summary>
        /// Путь к xml файлу
        /// </summary>
        private static string _path = "HighscoreData";

        /// <summary>
        /// Запись файла с данными о лидерах на диск
        /// </summary>
        public void Save()
        {
            var serializer = new XmlSerializer(typeof(HighScoreData));
            var stream = new FileStream(_path, FileMode.Create);
            serializer.Serialize(stream, this);
            stream.Close();
        }

        /// <summary>
        /// Считывание файла с данными о лидерах с диска
        /// </summary>
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

        /// <summary>
        /// Запись нового лидера. После записи список сортируется по времени, оставляя первых 10 лидирующих игроков
        /// </summary>
        /// <param name="gameType">Тип игры</param>
        /// <param name="name">Имя игрока</param>
        /// <param name="time">Время, за которое он прошел лабиринт</param>
        /// <param name="doneSteps">Количество сделаннх ним шагов</param>
        public static void WriteNewResult(GameType gameType, string name, int time, int doneSteps)
        {
            var data = Load();

            switch (gameType)
            {
                case GameType.Easy:
                    data.EasyHighScore = AddNewItemAndSortList(
                        data.EasyHighScore, new HighScoreItem(name, time, doneSteps));
                    break;

                case GameType.Medium:
                    data.MediumHighScore = AddNewItemAndSortList(
                        data.MediumHighScore, new HighScoreItem(name, time, doneSteps));
                    break;

                case GameType.Hard:
                    data.HardHighScore = AddNewItemAndSortList(
                        data.HardHighScore, new HighScoreItem(name, time, doneSteps));
                    break;

                default:
                    throw new ArgumentOutOfRangeException("gameType", gameType, null);
            }

            data.Save();
        }

        /// <summary>
        /// Добавляет нового лидера в список, сортирует его, и убирает всех, кто не попал в топ 10 по времени
        /// </summary>
        /// <param name="list">Список лидеров</param>
        /// <param name="newItem">Новый лидер</param>
        private static List<HighScoreItem> AddNewItemAndSortList(List<HighScoreItem> list, HighScoreItem newItem)
        {
            list.Add(newItem);
            var orderedList = list.OrderBy(item => item.Time).ToList();
            list.Clear();

            for (int i = 0; i < 10; i++)
            {
                if (i >= orderedList.Count) break;
                list.Add(orderedList[i]);
            }

            return list;
        }

        /// <summary>
        /// Сбрасывает все результаты лидеров
        /// </summary>
        public static void ClearResults()
        {
            var data = new HighScoreData();
            data.Save();
        }
    }
}