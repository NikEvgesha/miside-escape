using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private EnemyAI _enemy;
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
    }

    public void OnGameWin() {
        Debug.Log("WIN");
        _roundTime = Time.time - _roundTimeStart;

        GameWin?.Invoke(_roundTime);
    }


    public void OnGameLose()
    {
        GameLose?.Invoke();
    }


    public void OnGameRestart()
    {
        GameRestart?.Invoke();
        _roundTimeStart = Time.time;
        _player.transform.position = _startPlayerPosition;
        _enemy.gameObject.transform.position = _startEnemyPosition;
        // reset enemy ?
    }





}
