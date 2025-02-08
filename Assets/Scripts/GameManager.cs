using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private EnemyAI _enemy;
    [SerializeField] private bool _testMode;
    private Vector3 _startPlayerPosition;
    private Vector3 _startEnemyPosition;

    private float _roundTimeStart;
    private float _roundTime;
    private static GameManager _instance;

    public Action<float> GameWin;
    public Action GameLose;
    public Action GameRestart;
    public Action Reset;
    public static GameManager Instance { get { return _instance; } private set { } }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _roundTimeStart = Time.time;
        _startPlayerPosition = _player.transform.position;
        _startEnemyPosition = _enemy.transform.position;
    }

    public void OnGameWin() {
        _roundTime = Time.time - _roundTimeStart;
        SaveManager.Instance.SaveScore((int)_roundTime);
        _player.gameObject.SetActive(false);
        _enemy.gameObject.SetActive(false);
        Reset?.Invoke();
        GameWin?.Invoke(_roundTime);
    }


    public void OnGameLose()
    {
        if (!_testMode)
        {
            Reset?.Invoke();
            GameLose?.Invoke();
            _player.gameObject.SetActive(false);
            _enemy.gameObject.SetActive(false);
        }
        
    }


    public void OnGameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
