using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
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


    private EnemyState _enemyState = EnemyState.Idle;
    private NavMeshAgent _agent;
    private bool _isStart = false;
    [SerializeField] private float _timer = 0f;


    void Start()
    {
        if (_animator is null) { _animator =  GetComponentInChildren<Animator>(); }
        _player = FindAnyObjectByType<MovementController>().gameObject.transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _baseSpeed;  // Начальная скорость
        _agent.isStopped = true;
        _agent.SetDestination(_player.position);
        StartCoroutine(CheckState());
        StartCoroutine(Test());
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
                        _enemyState = EnemyState.Run;
                        _animator.SetTrigger(_animationRun);
                        _agent.isStopped = false; 
                        _agent.speed = _baseSpeed; 
                    }
                    break;
                case EnemyState.Run:
                    _timer += _aiFPS;
                    if (_timer >= _cooldown)
                    {
                        _enemyState = EnemyState.Wait;
                        _animator.SetTrigger(_animationWait);
                        _agent.isStopped = true;
                        _timer = 0;
                    }
                    break;
                case EnemyState.Wait:
                    _timer += _aiFPS;
                    if (_timer >= _stopTime)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.y);
                        _enemyState = EnemyState.FastRun;
                        _animator.SetTrigger(_animationFastRun);
                        _agent.isStopped = false;
                        _agent.speed = _boostSpeed;
                        _timer = 0;
                    }
                    break;
                case EnemyState.FastRun:
                    _timer += _aiFPS;
                    if (_timer >= _runTime)
                    {
                        _enemyState = EnemyState.Run;
                        _animator.SetTrigger(_animationRun);
                        _agent.speed = _baseSpeed;
                        _timer = 0;
                    }
                    break;
            }
            _agent.SetDestination(_player.position);
            yield return new WaitForSeconds(_aiFPS);
        }
    }
    public void StartSeek()
    {
        _isStart = true;
    }
    IEnumerator Test()
    {

        yield return new WaitForSeconds(10);

        StartSeek();
    }
}
