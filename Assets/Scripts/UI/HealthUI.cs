using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _healthIcon;

    private Health _playerHealth;

    private void Awake()
    {
        _playerHealth = gameObject.Player().health;
    }

    private void Start()
    {
        UpdateHealth();
    }

    private void OnEnable()
    {
        _playerHealth.OnTakeDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        _playerHealth.OnTakeDamage -= UpdateHealth;
    }

    private void UpdateHealth(int damage = 0)
    {
        if (transform.childCount > _playerHealth.health)
        {
            for (int i = _playerHealth.health; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        if (transform.childCount < _playerHealth.health)
        {
            var count = transform.childCount;
            for (int i = count; i < _playerHealth.health; i++)
            {
                Instantiate(_healthIcon, transform);
            }
        }
    }
}
