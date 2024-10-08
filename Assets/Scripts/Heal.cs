using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private Health _health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.AsPlayer() is PlayerRef player)
        {
            player.health.Heal(3);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
