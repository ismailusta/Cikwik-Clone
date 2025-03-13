using System;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public event Action OnCatCatched;
    [Header("References")]
    [SerializeField] PlayerController _playerController;
    [SerializeField] Transform _playerTransform;

    [Header("Settings")]
    [SerializeField] private float _defaultSpeed = 5f;
    [SerializeField] private float _chaseSpeed = 7f;

    [Header("Nav Settings")]
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _patrolRadius = 10f;
    [SerializeField] private int _maxAttempts = 10;
    [SerializeField] private float _chaseDistanceThreshold = 1.5f;
    [SerializeField] private float _chaseDistance = 2f;

    private Vector3 _initalPosition;
    private float _timer;
    private bool _isWaiting;
    private bool _isChasing;
    private NavMeshAgent _navMeshAgent;
    private CatStateController _catStateController;
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();
    }
    void Start()
    {
        _initalPosition = transform.position;
        SetRandomDestination();
    }
    void Update()
    {
        if (_playerController.canCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();
        }
    }
    private void SetChaseMovement()
    {
        _isChasing = true;
        _navMeshAgent.speed = _chaseSpeed;
        Vector3 directiontoPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 offSetPosition = _playerTransform.position - directiontoPlayer * _chaseDistanceThreshold;
        _navMeshAgent.SetDestination(offSetPosition);
        _catStateController.ChangeState(CatState.Running);

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseDistance && _isChasing)
        {
            // yakaladı
            OnCatCatched?.Invoke();
            _catStateController.ChangeState(CatState.Attacking);
            _isChasing = false;

        }
    }
    private void SetPatrolMovement()
    {
        _navMeshAgent.speed = _defaultSpeed;
        if (!_isWaiting)
        {
            _isWaiting = true;
            _timer = _waitTime;
            _catStateController.ChangeState(CatState.Idle);
        }
        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }
    }
    private void SetRandomDestination()
    {
        int attempts = 0;
        bool destinationSet = false;
        while (attempts < _maxAttempts && !destinationSet)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius;
            randomDirection += transform.position;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                destinationSet = true;
                if (!IsBlocked(finalPosition))
                {
                    _navMeshAgent.SetDestination(finalPosition);
                    destinationSet = true;
                }
                else
                {
                    attempts++;
                }
            }
            else
            {
                attempts++;
            }
        }
        if (!destinationSet)
        {
            _isWaiting = true;
            _timer = _waitTime * 2;
        }
    }
    private bool IsBlocked(Vector3 position)
    {
        if (NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }
}
