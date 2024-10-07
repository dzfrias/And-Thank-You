using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _healthIcon;

    private Health _playerHealth;

    private void Start()
    {
        UpdateHealth();
        _playerHealth.OnTakeDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        _playerHealth.OnTakeDamage -= UpdateHealth;
    }

    private void UpdateHealth(int damage = 0)
    {
        _playerHealth = gameObject.Player().health;
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
