using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerApproacher : MonoBehaviour
{
    
    
    [SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private ParticleSystem ps;
    
    [Header("SFX")]
    [SerializeField] private AudioClip hurtSFX;
    [SerializeField] private AudioClip attackSFX;
    
    public enum EnemyState
    {
        Idle,
        Walking,
        Attacking,
        Dead
    }
    
    private Animator _animator;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private EnemyState _currentState = EnemyState.Idle;
    private float _timeInState;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
        _audioSource.PlayOneShot(hurtSFX);
        SwitchState(EnemyState.Dead);
        JuiceController.Instance.Slowdown(1.5f);
        JuiceController.Instance.ScreenShake(0.3f, 0.3f);
        LevelManager.Instance.TriggerLevelWin(2.5f);
        
        ps.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SwitchState(EnemyState.Attacking);
            _audioSource.PlayOneShot(attackSFX);
            
            other.gameObject.GetComponent<Player>().KillPlayer();
            // Change to level loss
            LevelManager.Instance.TriggerLevelLoss(1.5f);
        }
    }
}
