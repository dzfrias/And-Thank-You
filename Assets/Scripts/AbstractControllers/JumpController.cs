using System;
using UnityEngine;

public abstract class JumpController : MovementController
{
    public event Action OnJump;

    protected void RaiseJumpEvent()
    {
        OnJump?.Invoke();
    }
}
