using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void KillPlayer()
    {
        LevelManager.Instance.IsInputEnabled = false;
        _audioSource.Play();
        _particleSystem.Play();
    }
}
