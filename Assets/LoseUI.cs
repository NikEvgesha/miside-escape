using UnityEngine;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;


    private void Start()
    {
        GameManager.Instance.GameLose += OpenLosePanel;

    }

    private void OpenLosePanel()
    {
        _losePanel.SetActive(true);

    }


    public void OnRestartButtonPressed()
    {
        GameManager.Instance.OnGameRestart();
    }
}
