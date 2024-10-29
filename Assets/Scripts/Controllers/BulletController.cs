using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BulletController : MonoBehaviour, IMovementController
{
    private Health _health;
    private int _direction = 1;
    [SerializeField] private int _hitSpeed;
    [SerializeField] private int _speed;
    private bool _hit = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnTakeDamage += DamageAction;
    }

    public void DamageAction(int dmg)
    {
        if (_hit) return;
        StartCoroutine(_FreezeGame());
        
    }

    private IEnumerator _FreezeGame()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(.3f);
        Time.timeScale = 1;
        _direction *= -_hitSpeed;
        _hit = true;
    }

    public float GetMovement()
    {
        return (transform.forward.x > 0 ? 1: -1) * _direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            var health = collision.gameObject.GetComponent<Health>();
            health.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
