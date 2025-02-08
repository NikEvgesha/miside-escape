using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Text _roundTime;
    [SerializeField] private GameObject _winPanel;
    //[SerializeField] private ScoreUISlot _scorePrefab;
    [SerializeField] private LBPlayerDataYG _lbSlotPrefab;
    [SerializeField] private LeaderboardYG _lbGlobal;
    [SerializeField] private Transform _playerScoreParent;


    private void Start()
    {
        GameManager.Instance.GameWin += OpenWinPanel;
    }

    private void OpenWinPanel(float time) {
        _winPanel.SetActive(true);
        SetTime(time);
        SetScores();

    }

    private void SetTime(float time) {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        _roundTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnRestartButtonPressed() {
        Debug.Log("Restart");
        GameManager.Instance.OnGameRestart();
    }

    public void SetScores() {
        List<int> scores = SaveManager.Instance.GetPlayerScores();
            while (_playerScoreParent.childCount > 0)
            {
                DestroyImmediate(_playerScoreParent.GetChild(0).gameObject);
            }
        int i = 1;
        foreach (int score in scores) {
            var slot = Instantiate(_lbSlotPrefab, _playerScoreParent);
            slot.data.thisPlayer = true;
            slot.data.score = string.Format("{0:00}:{1:00}", score/60, score%60);
            slot.data.photoSprite = _lbGlobal.isHiddenPlayerPhoto;
            slot.data.rank = i.ToString();
            i++;
            slot.UpdateEntries();
        }
    }
}
