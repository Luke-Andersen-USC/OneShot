using System;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    protected virtual void Awake()
    {
        PlayerInputManager playerInputManager = GetComponentInParent<PlayerInputManager>();
        playerInputManager.OnShootStart += ShootStart;
        playerInputManager.OnShootEnd += ShootEnd;
    }

    protected virtual void ShootStart()
    {
        Debug.Log("Shot started");
    }

    protected virtual void ShootEnd()
    {
        Debug.Log("Shot ended");
    }
}
