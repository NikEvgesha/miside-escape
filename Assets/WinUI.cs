using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Text _roundTime;
    [SerializeField] private GameObject _winPanel;
    //[SerializeField] private ScoreUISlot _scorePrefab;
    [SerializeField] private LBPlayerDataYG _lbSlotPrefab;
    [SerializeField] private LeaderboardYG _lbGlobal;
    [SerializeField] private LeaderboardYG _lbMonth;
    [SerializeField] private Transform _playerScoreParent;


    private void Start()
    {
        GameManager.Instance.GameWin += OpenWinPanel;
    }

    private void OpenWinPanel(float time) {
        _winPanel.SetActive(true);
        SetTime(time);
        SetScores();
        _lbGlobal.UpdateLB();
        _lbMonth.UpdateLB();
    }

    private void SetTime(float time) {
        var timeSpan = TimeSpan.FromMilliseconds(time*1000);
        _roundTime.text = string.Format("{0:D2}:{1:D2}.{2:000}", (int)timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    public void OnRestartButtonPressed() {
        Debug.Log("Restart");
        GameManager.Instance.OnGameRestart();
    }

    public void SetScores() {
        List<float> scores = SaveManager.Instance.GetPlayerScores();
            while (_playerScoreParent.childCount > 0)
            {
                DestroyImmediate(_playerScoreParent.GetChild(0).gameObject);
            }
        int i = 1;
        foreach (float score in scores) {
            var slot = Instantiate(_lbSlotPrefab, _playerScoreParent);
            slot.data.thisPlayer = true;
            var timeSpan = TimeSpan.FromMilliseconds(score*1000);
            slot.data.score = string.Format("{0:D2}:{1:D2}.{2:000}", (int)timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
            slot.data.photoSprite = _lbGlobal.isHiddenPlayerPhoto;
            slot.data.rank = i.ToString();
            i++;
            slot.UpdateEntries();
        }
    }
}
