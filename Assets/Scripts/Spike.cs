using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spike : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        var health = collision.collider.transform.GetComponent<Health>();
        if (collision.collider.CompareTag("player"))
        {
            health.TakeDamage(1);
            return;
        }
        health.Kill();
    }
}
