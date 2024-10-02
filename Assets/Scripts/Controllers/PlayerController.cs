using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMovementController, IJumpController
{
    public event Action OnJump;
    public float sprintSpeed;

    public float GetMovement()
    {
        float sprint = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : 1;
        return Input.GetAxisRaw("Horizontal") * sprint;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }
    }
}
