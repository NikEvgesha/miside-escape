using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private EnemyAI _enemy;
    [SerializeField] private bool _testMode;

    private float _roundTimeStart;
    private float _roundTime;
    private static GameManager _instance;

    public Action<float> GameWin;
    public Action GameLose;
    public Action GameRestart;
    public Action Reset;

    private bool _inProgress;
    public bool GameInProgress { get { return _inProgress; } private set { } }
    public static GameManager Instance { get { return _instance; } private set { } }

    private void Awake()
    {
        _instance = this;
    }
    private void OnEnable()
    {
        YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;
    }
    private void OnDisable()
    {
        YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
    }
    private void Start()
    {
        _roundTimeStart = Time.time;
        _inProgress = true;
        YandexGame.GameplayStart();
    }
    private void OnVisibilityWindowGame(bool _isVisible)
    {
        Time.timeScale = _isVisible ? 1f : 0f;
        AudioListener.pause = !_isVisible;
    }
    public void OnGameWin() {
        _roundTime = Time.time - _roundTimeStart;
        SaveManager.Instance.SaveScore(_roundTime);
        Reset?.Invoke();
        GameWin?.Invoke(_roundTime);
        _inProgress = false;
        Destroy(_enemy.gameObject);
        YandexGame.GameplayStop();
    }
    public void OnGameLose()
    {
        if (!_testMode)
        {
            Reset?.Invoke();
            GameLose?.Invoke();
            _inProgress = false;
            Destroy(_enemy.gameObject);
            YandexGame.GameplayStop();
        }
        
    }
    public void OnGameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
