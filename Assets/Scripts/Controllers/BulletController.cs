using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IMovementController
{
    public float GetMovement()
    {
        return (transform.forward.x > 0 ? 1: -1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.AsPlayer() is PlayerRef player)
        {
            var playerHealth = player.health;
            playerHealth.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
