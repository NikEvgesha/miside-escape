using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(ChainsawSoundController))]
public class EnemyAI : MonoBehaviour
{

    [SerializeField] private Transform _player;   // Ссылка на игрока
    [SerializeField] private float _baseSpeed = 3.5f;   
    [SerializeField] private float _boostSpeed = 6f;    
    [SerializeField] private float _stopTime = 2f;
    [SerializeField] private float _runTime = 5f;
    [SerializeField] private float _cooldown = 20f;
    [SerializeField] private float _aiFPS = 1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationRun = "Run";
    [SerializeField] private string _animationWait = "Wait";
    [SerializeField] private string _animationFastRun = "FustRun";
    [SerializeField] private Door _door;
    [SerializeField] private ChainsawSoundController chainsawSoundController;

    private static EnemyAI _instance;
    private EnemyState _enemyState = EnemyState.Idle;
    private NavMeshAgent _agent;
    private bool _isStart = false;
    [SerializeField] private float _timer = 0f;

    public Action SawPrepare;
    public Action AttackStart;
    public Action AttackEnd;
    public static EnemyAI Instance {  get { return _instance; } }

    private void Awake()
    {
        _instance = this;
        chainsawSoundController = GetComponent<ChainsawSoundController>();
    }


    void Start()
    {
        _door.DoorOpen += StartSeek;
        if (_animator is null) { _animator =  GetComponentInChildren<Animator>(); }
        _player = FindAnyObjectByType<MovementController>().gameObject.transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _baseSpeed;  // Начальная скорость
        _agent.isStopped = true;
        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {
        while (true)
        {
            switch (_enemyState)
            {
                case EnemyState.Idle:
                    if (_isStart)
                    {
                        chainsawSoundController.PlayIdleSound();
                        _enemyState = EnemyState.Run;
                        //_animator.SetTrigger(_animationRun);
                        _agent.isStopped = false; 
                        _agent.speed = _baseSpeed; 
                    }
                    break;
                case EnemyState.Run:
                    _timer += _aiFPS;
                    if (_timer >= _cooldown)
                    {
                        chainsawSoundController.PlayStartSound();
                        _enemyState = EnemyState.Wait;
                        _animator.SetTrigger(_animationWait);
                        _agent.isStopped = true;
                        _timer = 0;
                        SawPrepare?.Invoke();
                    }
                    break;
                case EnemyState.Wait:
                    _timer += _aiFPS;
                    if (_timer >= _stopTime)
                    {
                        chainsawSoundController.PlayFullPowerSound();
                        _animator.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
                        _enemyState = EnemyState.FastRun;
                        _animator.SetTrigger(_animationFastRun);
                        _agent.isStopped = false;
                        _agent.speed = _boostSpeed;
                        _timer = 0;
                        AttackStart?.Invoke();
                    }
                    break;
                case EnemyState.FastRun:
                    _timer += _aiFPS;
                    if (_timer >= _runTime)
                    {
                        chainsawSoundController.PlayIdleSound();
                        _enemyState = EnemyState.Run;
                        _animator.SetTrigger(_animationRun);
                        _agent.speed = _baseSpeed;
                        _timer = 0;
                        AttackEnd?.Invoke();
                    }
                    break;
            }
            _agent.SetDestination(_player.position);
            yield return new WaitForSeconds(_aiFPS);
        }
    }
    private void StartSeek()
    {
        _isStart = true;
        _agent.SetDestination(_player.position);
    }
    public void Pause()
    {
        _isStart = false;
        _enemyState = EnemyState.Idle;
        _animator.SetTrigger(_animationRun);
        _animator.SetTrigger(_animationWait);
        chainsawSoundController.StopSound();
    }
}
