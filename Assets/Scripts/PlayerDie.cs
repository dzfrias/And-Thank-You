using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class PlayerDie : MonoBehaviour
{
    private Health _health;
    private bool isInvincible = false;
    [SerializeField] private float _blinkSpeed;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDie += DieAction;
        _health.OnTakeDamage += DamageAction;
    }

    private void DieAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void DamageAction() 
    {
        StartCoroutine(_Invincibility(1f));
    }

    private IEnumerator _Invincibility(float time)
    {
        isInvincible = true;
        var remainder = time % _blinkSpeed;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < Mathf.Floor(time / _blinkSpeed); i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(_blinkSpeed);
        }
        yield return new WaitForSeconds(remainder);
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(_health.health);
        
        if (isInvincible)
        {
            return;
        }
        if (collision.collider.CompareTag("playerDies") || collision.collider.CompareTag("spike"))
        {
            _health.TakeDamage(1);
        }
    }
}
