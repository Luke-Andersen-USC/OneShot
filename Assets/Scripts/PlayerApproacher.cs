using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerApproacher : MonoBehaviour
{
    
    
    [SerializeField] private float walkSpeed = 0.5f;
    public enum EnemyState
    {
        Idle,
        Walking,
        Attacking,
        Dead
    }
    
    private Animator _animator;
    private Rigidbody _rigidbody;

    private EnemyState _currentState = EnemyState.Idle;
    private float _timeInState;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SwitchState(EnemyState.Walking);
    }

    void Update()
    {
        _timeInState += Time.deltaTime;

        if (_currentState == EnemyState.Walking)
        {
            transform.Translate(-Vector3.left * walkSpeed * Time.deltaTime);
        }
    }

    public void SwitchState(EnemyState newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;
        _timeInState = 0f;
        
        _animator.SetTrigger(_currentState.ToString());
        Debug.Log($"Switched to {_currentState}");
    }
    
    public EnemyState currentState => _currentState;
    public float TimeInState => _timeInState;

    public void KillEnemy()
    {
        SwitchState(EnemyState.Dead);
        LevelManager.Instance.TriggerLevelWin(1.5f);
    }
    
    
    // UI Manager One Shot
    
    // UI Manager Retry
    
    // UI Manager Score Screen
}
