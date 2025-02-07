using System;
using UnityEngine;

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

        GameWin?.Invoke(_roundTime);
    }


    public void OnGameLose()
    {
        Debug.Log("LOSE");
        if (!_testMode)
        {
            GameLose?.Invoke();
        }
        
    }


    public void OnGameRestart()
    {
        GameRestart?.Invoke();
        _roundTimeStart = Time.time;
        _player.transform.position = _startPlayerPosition; // перенести в player

        _enemy.gameObject.transform.position = _startEnemyPosition; // перенести в enemy + ресет задержки
    }





}
