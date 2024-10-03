using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public event Action OnDie;
    private int health;


    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDie?.Invoke();
            health = 0;
        }
    }

    public void Heal(int healthHealed,bool canOverheal)
    {
        health += healthHealed;
        if (health > maxHealth && !canOverheal)
        {
            health = maxHealth;
        }
    }
}
