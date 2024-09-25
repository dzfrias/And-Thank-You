using System;
using UnityEngine;

public class PlayerController : JumpController
{
    public float sprintSpeed;

    public override float GetMovement()
    {
        float sprint = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : 1;
        return Input.GetAxisRaw("Horizontal") * sprint;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaiseJumpEvent();
        }
    }
}
