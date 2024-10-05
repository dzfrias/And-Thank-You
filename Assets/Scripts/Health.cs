using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDie;
    public event Action<int> OnTakeDamage;

    [SerializeField] private int maxHealth;
    [HideInInspector] public bool isInvincible;
    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        health -= damage;
        if (health <= 0)
        {
            OnDie?.Invoke();
            health = 0;
        }
        else
        {
            OnTakeDamage?.Invoke(damage);
        }
        
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }

    public void Kill()
    {
        health = 0;
        OnDie?.Invoke();
    }
}
