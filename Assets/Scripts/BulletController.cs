using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MovementController
{
    public override float GetMovement()
    {
        return 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
