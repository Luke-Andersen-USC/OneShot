using System;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    private void Awake()
    {
        PlayerInputManager playerInputManager = GetComponentInParent<PlayerInputManager>();
        playerInputManager.OnShootStart += ShootStart;
        playerInputManager.OnShootEnd += ShootEnd;
    }

    public virtual void ShootStart()
    {
        Debug.Log("Shot started");
    }

    public virtual void ShootEnd()
    {
        Debug.Log("Shot ended");
    }
}
