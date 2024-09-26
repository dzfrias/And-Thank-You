using System;
using UnityEngine;

public abstract class ShootProjectileController : JumpController
{
    public event Action OnFire;

    protected void RaiseFireEvent()
    {
        OnFire?.Invoke();
    }
}
