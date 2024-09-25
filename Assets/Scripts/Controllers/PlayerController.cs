using System;
using UnityEngine;

public class PlayerController : JumpController
{
    public override float GetMovement()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaiseJumpEvent();
        }
    }
}
