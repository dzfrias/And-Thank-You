using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float friction { get; private set; }
    public Transform ground { get; private set; }

    public bool onGround
    {
        get
        {
            return ground != null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        EvaluateCollision(collision);
        GetFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        EvaluateCollision(collision);
        GetFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        friction = 0;
        ground = null;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (normal.y >= 0.9f)
            {
                ground = collision.transform;
            }
        }
    }

    private void GetFriction(Collision2D collision)
    {
        if (!collision.rigidbody || !collision.rigidbody.sharedMaterial) return;
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;
        friction = 0;
        if (material != null)
        {
            friction = material.friction;
        }
    }
}
