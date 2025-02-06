using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public Action<float> GameWin;
    public Action GameLose;
    public Action GameRestart;

    private float _roundTimeStart;
    private float _roundTime;
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } private set { } }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _roundTimeStart = Time.time;
    }

    public void onGameWin() {
        Debug.Log("WIN");
        _roundTime = Time.time - _roundTimeStart;

        GameWin?.Invoke(_roundTime);
    }


    public void onGameLose()
    {
        GameLose?.Invoke();
    }





}
