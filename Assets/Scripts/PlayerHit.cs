using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class PlayerHit : MonoBehaviour
{
    public float invincibilityTime = 1f;
    public float blinkSpeed = 0.1f;

    private Health _health;

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

    private void DamageAction(int damage) 
    {
        StartCoroutine(Invincibility());
    }

    private IEnumerator Invincibility()
    {
        _health.isInvincible = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.4f);
        Time.timeScale = 1f;
        var remainder = invincibilityTime % blinkSpeed;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < Mathf.Floor(invincibilityTime / blinkSpeed); i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
        yield return new WaitForSeconds(remainder);
        spriteRenderer.enabled = true;
        _health.isInvincible = false;
    }
}
