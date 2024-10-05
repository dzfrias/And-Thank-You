using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground), typeof(Collider2D), typeof(Health))]
public class EnemyController : MonoBehaviour, IMovementController
{
    public float stunTime = 0.2f;

    private Ground _ground;
    private Collider2D _collider;
    private Health _health;
    private float _movement = 1f;
    private float stun;

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

        var groundCollider = _ground.ground.GetComponent<Collider2D>();
        if ((_collider.Right() > groundCollider.Right() && _movement > 0) || (_collider.Left() < groundCollider.Left() && _movement < 0))
        {
            Flip();
        }
        stun = Mathf.Max(stun - Time.deltaTime, 0f);
    }

    public float GetMovement()
    {
        if (!_ground.onGround || stun > 0) return 0;
        return _movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("player"))
        {
            var playerHealth = collision.collider.transform.GetComponent<Health>();
            playerHealth.TakeDamage(1);
            return;
        }
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (normal.x != 0)
            {
                _movement = normal.x > 0 ? 1 : -1;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("player")) return;
        var playerHealth = collision.collider.transform.GetComponent<Health>();
        playerHealth.TakeDamage(1);
    }

    private void Flip()
    {
        _movement *= -1;
    }

    private void OnTakeDamage(int damage)
    {
        stun += stunTime;
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
