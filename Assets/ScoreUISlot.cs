using UnityEngine;
using UnityEngine.UI;

public class ScoreUISlot : MonoBehaviour
{
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _timeScore;


    public void SetData(int time, string name = null) {
        _timeScore.text = string.Format("{0:00}:{1:00}", time/60, time%60);
        if (name != null)
        {
            _playerName.text = name;
        }
    }
}
