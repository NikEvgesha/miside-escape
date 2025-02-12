using UnityEngine;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        GameManager.Instance.GameLose += OpenLosePanel;
        
    }

    private void OpenLosePanel()
    {
        _losePanel.SetActive(true);
        _animator.SetTrigger("Open");

    }


    public void OnRestartButtonPressed()
    {
        GameManager.Instance.OnGameRestart();
    }
}
