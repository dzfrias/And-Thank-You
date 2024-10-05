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
}
