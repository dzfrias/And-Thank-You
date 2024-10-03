using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class PlayerDie : MonoBehaviour
{
    private Health _health;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int _knockback_x;
    [SerializeField] private int _knockback_y;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDie += DieAction;
    }

    private void DieAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("playerDies"))
        {
            _health.TakeDamage(1);
            //transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            _rb.AddForce(new Vector2(_knockback_x * -(transform.forward.x > 0 ? 1 : -1), _knockback_y));
        }
    }
}
