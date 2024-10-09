using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMovementController, IJumpController, IAttackController
{
    public event Action OnJump;
    public event Action OnAttack;

    [SerializeField] private float attackCooldown = 0.5f;

    private bool canAttack = true;

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
        if (Input.GetKeyDown(KeyCode.F) && canAttack)
        {
            OnAttack?.Invoke();
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
