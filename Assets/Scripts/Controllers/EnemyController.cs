using UnityEngine;

[RequireComponent(typeof(Ground), typeof(Collider2D))]
public class EnemyController : MonoBehaviour, IMovementController
{
    private Ground _ground;
    private Collider2D _collider;
    private float _movement = 1f;

    private void Start()
    {
        _ground = GetComponent<Ground>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!_ground.onGround) return;

        var groundCollider = _ground.ground.GetComponent<Collider2D>();
        if ((_collider.Right() > groundCollider.Right() && _movement > 0) || (_collider.Left() < groundCollider.Left() && _movement < 0))
        {
            Flip();
        }
    }

    public float GetMovement()
    {
        if (!_ground.onGround) return 0;
        return _movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (normal.x != 0)
            {
                _movement = normal.x > 0 ? 1 : -1;
            }
        }
    }

    private void Flip()
    {
        _movement *= -1;
    }
}
