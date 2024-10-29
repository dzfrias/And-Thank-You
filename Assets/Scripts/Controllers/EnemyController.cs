using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Ground), typeof(Collider2D), typeof(Health))]
public class EnemyController : MonoBehaviour, IMovementController
{
    public float stunTime = 0.2f;

    private Ground _ground;
    private Collider2D _collider;
    private Health _health;
    private float _movement = 1f;
    private float _stun;


    private void Awake()
    {
        _ground = GetComponent<Ground>();
        _collider = GetComponent<Collider2D>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.OnTakeDamage += OnTakeDamage;
        _health.OnDie += OnDie;
    }

    private void OnDisable()
    {
        _health.OnTakeDamage -= OnTakeDamage;
        _health.OnDie -= OnDie;
    }

    private void Update()
    {
        if (!_ground.onGround) return;

        if ((!Physics2D.Raycast(new Vector2(_collider.Right() + 0.1f, _collider.Bottom()), Vector2.down, 0.1f) && _movement > 0) || (!Physics2D.Raycast(new Vector2(_collider.Left() - 0.1f, _collider.Bottom()), Vector2.down, 0.1f) && _movement < 0))
        {
            Flip();
        }
        var groundCollider = _ground.ground.GetComponent<Collider2D>();
        if ((_collider.Right() > groundCollider.Right() && _movement > 0) || (_collider.Left() < groundCollider.Left() && _movement < 0))
        {
            Flip();
        }
        _stun = Mathf.Max(_stun - Time.deltaTime, 0f);
    }

    public float GetMovement()
    {
        if (!_ground.onGround || _stun > 0) return 0;
        return _movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collide(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Collide(collision);
    }
    
    private void Collide(Collision2D collision)
    {
        TryDoDamage(collision);
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (normal.x != 0)
            {
                _movement = normal.x > 0 ? 1 : -1;
            }
        }
    }

    private void TryDoDamage(Collision2D collision)
    {
        if (collision.gameObject.AsPlayer() is PlayerRef player)
        {
            var playerHealth = player.health;
            playerHealth.TakeDamage(1);
            return;
        }
    }

    private void Flip()
    {
        _movement *= -1;
    }

    private void OnTakeDamage(int damage)
    {
        _stun += stunTime;
    }

    private void OnDie()
    {
        StartCoroutine(_OnDie());
    }

    private IEnumerator _OnDie()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}
