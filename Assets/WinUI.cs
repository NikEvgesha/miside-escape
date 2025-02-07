using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Text _roundTime;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private ScoreUISlot _scorePrefab;
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
        foreach (int score in scores) {
            var slot = Instantiate(_scorePrefab, _playerScoreParent);
            slot.SetData(score);
        }
    }
}
