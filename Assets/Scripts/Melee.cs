using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Vector2 attackOffset;
    public Vector2 attackSize = new Vector2(1, 2);
    public float hitForce;

    private Direction _direction;

    private void Awake()
    {
        _direction = GetComponent<Direction>();
    }

    public void Attack()
    {
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        float k = _direction?.AsSign() ?? 1;
        Collider2D collider = Physics2D.OverlapBox(transform.position + new Vector3(attackOffset.x * k, attackOffset.y, 0), attackSize, 0f, layerMask);
        if (!collider) return;
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
