using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAttackController))]
public class Melee : MonoBehaviour
{
    public Vector2 attackOffset;
    public Vector2 attackSize = new Vector2(1, 2);
    public float hitForce;
    [Range(0f, 1f)] public float delay;

    private IAttackController _controller;
    private Direction _direction;

    private void Awake()
    {
        _controller = GetComponent<IAttackController>();
        _direction = GetComponent<Direction>();
    }

    private void OnEnable()
    {
        _controller.OnAttack += Attack;
    }

    private void OnDisable()
    {
        _controller.OnAttack -= Attack;
    }

    private void Attack()
    {
        StartCoroutine(_Attack());
    }

    private IEnumerator _Attack()
    {
        yield return new WaitForSeconds(delay);
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        float k = _direction?.AsSign() ?? 1;
        Collider2D collider = Physics2D.OverlapBox(transform.position + new Vector3(attackOffset.x * k, attackOffset.y, 0), attackSize, 0f, layerMask);
        if (!collider) yield break;
        var health = collider.transform.GetComponent<Health>();
        health.TakeDamage(1);
        if (hitForce != 0)
        {
            collider.transform.GetComponent<Rigidbody2D>()?.AddForce(Vector2.right * k * hitForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Vector3 offset = attackOffset;
        Gizmos.DrawCube(transform.position + offset, attackSize);
    }
}
