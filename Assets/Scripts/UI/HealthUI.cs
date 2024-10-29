using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Image))]
public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> _images;

    private Health _playerHealth;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _playerHealth = gameObject.Player().health;
        UpdateHealth();
        _playerHealth.OnTakeDamage += UpdateHealth;
        _playerHealth.OnHeal += UpdateHealth;
    }

    private void OnDisable()
    {
        _playerHealth.OnTakeDamage -= UpdateHealth;
        _playerHealth.OnHeal -= UpdateHealth;
    }

    private void UpdateHealth(int damage = 0)
    {
        Health health = gameObject.Player().health;
        Sprite sprite = _images[health.health - 1];
        _image.sprite = sprite;
    }
}
