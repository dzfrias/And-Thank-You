using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IShootProjectileController, IMovementController, IJumpController
{
    public event Action OnJump;
    public event Action OnFire;

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
            OnFire?.Invoke();
        }
    }
}
