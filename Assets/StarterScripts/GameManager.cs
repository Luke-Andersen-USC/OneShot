using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private static GameObject player;

    public static GameObject Player
    {
        get
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            return player;
        }
    }
    
    private static GameObject boss;
    public static GameObject Boss
    {
        get
        {
            if (boss == null)
            {
                boss = GameObject.Find("Boss");
            }
            return boss;
        }
    }


    /*
    private PlayerController playerController;

    public PlayerController PlayerController
    {
        get
        {
            if (playerController == null)
            {
                playerController = Player.GetComponentInChildren<PlayerController>();
            }
            return playerController;
        }
    }
    */
    
    private UIController uiController;
    public UIController UIController
    {
        get
        {
            if (uiController == null)
            {
                uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
            }
            return uiController;
        }
    }
    
    
    public static void Setup()
    {
        if (Gamepad.all.Count < 2)
        {
            Debug.LogError("Need two controllers!");
            return;
        }

        // Get PlayerInput components
        var playerInput = Player.GetComponentInChildren<UnityEngine.InputSystem.PlayerInput>();
        var bossInput   = Boss.GetComponentInChildren<UnityEngine.InputSystem.PlayerInput>();

        // Assign devices
        PairDevice(playerInput, Gamepad.all[0]);
        PairDevice(bossInput, Gamepad.all[1]);
    }

    private static void PairDevice(UnityEngine.InputSystem.PlayerInput input, InputDevice device)
    {
        if (input == null || device == null)
        {
            Debug.LogError("Cannot pair device: null reference");
            return;
        }

        var user = input.user;
        user.UnpairDevices(); 
        InputUser.PerformPairingWithDevice(device, user);

        // Switch the current control scheme to match the device
        //input.SwitchCurrentControlScheme(device);

        Debug.Log($"Paired {input.gameObject.name} to {device.displayName}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}