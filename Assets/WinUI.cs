using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Text _roundTime;
    [SerializeField] private GameObject _winPanel;


    private void Start()
    {
        GameManager.Instance.GameWin += OpenWinPanel;
    }

    private void OpenWinPanel(float time) {
        _winPanel.SetActive(true);
        SetTime(time);

    }

    private void SetTime(float time) {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        _roundTime.text = minutes + ":" + seconds;
    }

    public void OnRestartButtonPressed() {
        Debug.Log("Restart");
    }
}
