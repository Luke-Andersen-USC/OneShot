using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private Canvas _gameplayCanvas;
    [SerializeField] private Canvas _levelEndCanvas;
    [SerializeField] private Canvas _titleCanvas;
    [SerializeField] private Canvas _pauseMenuCanvas;
    
    [Header("Misc")]
    [SerializeField] private TextMeshProUGUI _winText;

    [Header("Input")]
    [SerializeField] private InputActionReference _pauseAction;

    private bool _isPaused;
    
    // Prototype vars
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        _pauseAction.action.performed += OnPausePressed;
        _pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        _pauseAction.action.performed -= OnPausePressed;
        _pauseAction.action.Disable();
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        
        Time.timeScale = _isPaused ? 0f : 1f;

        EnablePauseMenu(_isPaused);
    }

    public void EnablePauseMenu(bool enable)
    {
        _pauseMenuCanvas.enabled = enable;
    }

    public void EnableWinCanvas(bool enable)
    {
        _levelEndCanvas.enabled = enable;
    }
}