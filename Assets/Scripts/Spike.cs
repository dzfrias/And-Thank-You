using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spike : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.AsPlayer() is PlayerRef player)
        {
            var playerHealth = player.health;
            playerHealth.TakeDamage(1);
            return;
        }
        var health = collision.collider.transform.GetComponent<Health>();
        health?.Kill();
    }
}
