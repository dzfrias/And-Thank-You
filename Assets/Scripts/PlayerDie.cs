using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class PlayerDie : MonoBehaviour
{
    [SerializeField] private float _blinkSpeed;
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
        StartCoroutine(_Invincibility(1f));
    }

    private IEnumerator _Invincibility(float time)
    {
        _health.isInvincible = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.4f);
        Time.timeScale = 1f;
        var remainder = time % _blinkSpeed;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < Mathf.Floor(time / _blinkSpeed); i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(_blinkSpeed);
        }
        yield return new WaitForSeconds(remainder);
        spriteRenderer.enabled = true;
        _health.isInvincible = false;
    }
}
