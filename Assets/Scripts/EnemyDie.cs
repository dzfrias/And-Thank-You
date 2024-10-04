using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyDie : MonoBehaviour
{ 
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDie += DieAction;
    }

    private void DieAction()
    {
        StartCoroutine(_OnDie());
    }

    private IEnumerator _OnDie()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "Enemy Dies" && collision.collider.name == "Player")
        {
            _health.TakeDamage(1000);
            return;
        }
        if (collision.collider.CompareTag("spike"))
        {
            _health.TakeDamage(1000);
        }
    }

}
