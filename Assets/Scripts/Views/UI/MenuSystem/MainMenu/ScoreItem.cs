using LabyrinthEscape.HighScoreControls;
using LabyrinthEscape.SomeHelpers;
using UnityEngine;
using UnityEngine.UI;

namespace LabyrinthEscape.MenuSystem
{
    public class ScoreItem : MonoBehaviour
    {
        [SerializeField] private Text _nameText;

        [SerializeField] private Text _timeText;

        public void Init(HighScoreItem highScoreItem, int position)
        {
            _nameText.text = string.Format("{0}. {1}", position, highScoreItem.Name);
            _timeText.text = Helpers.GetFormattedTimeFromSeconds(highScoreItem.Time) + ", Steps: " +
                             highScoreItem.DoneSteps;
        }
    }
}