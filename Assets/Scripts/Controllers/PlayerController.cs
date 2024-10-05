using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMovementController, IJumpController, IAttackController
{
    public event Action OnJump;
    public event Action OnAttack;

    public float GetMovement()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnAttack?.Invoke();
        }
    }
}
