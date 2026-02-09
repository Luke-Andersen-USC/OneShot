using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public event Action OnShootStart;
    public event Action OnShootEnd;

    public PlayerInput PlayerInput { get; private set; }

    private InputAction _shootAction;

    protected void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        if (PlayerInput == null)
        {
            Debug.LogError("PlayerInput component missing!");
            return;
        }

        PlayerInput.SwitchCurrentActionMap("Player");

        var actions = PlayerInput.actions;

        _shootAction = actions["Shoot"];
        _shootAction.performed += OnShootInputStart;
        _shootAction.canceled += OnShootInputEnd;
    }

    private void OnDestroy()
    {
        if (_shootAction != null)
        {
            _shootAction.performed -= OnShootInputStart;
            _shootAction.canceled -= OnShootInputEnd;
        }
    }

    private void OnShootInputStart(InputAction.CallbackContext ctx)
    {
        OnShootStart?.Invoke();
    }

    private void OnShootInputEnd(InputAction.CallbackContext ctx)
    {
        OnShootEnd?.Invoke();
    }
}