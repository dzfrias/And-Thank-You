using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GunEnemyController : MonoBehaviour, IMovementController, IAttackController
{
    [SerializeField] private int _moveSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootSpeed;
    private PlayerRef _player;

    public float stunTime = 0.2f;

    private Ground _ground;
    private Collider2D _collider;
    private Health _health;
    private float stun;

    public event Action OnAttack;

    private void Start()
    {
        _player = gameObject.Player();
        StartCoroutine(_Attack());
    }

    private IEnumerator _Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_shootSpeed);
            OnAttack?.Invoke();
        }
    }

    private void Awake()
    {
        _ground = GetComponent<Ground>();
        _collider = GetComponent<Collider2D>();
        _health = GetComponent<Health>();

    }

    private void OnEnable()
    {
        _health.OnTakeDamage += OnTakeDamage;
        _health.OnDie += OnDie;
    }

    private void OnDisable()
    {
        _health.OnTakeDamage -= OnTakeDamage;
        _health.OnDie -= OnDie;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDoDamage(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryDoDamage(collision);
    }

    private void TryDoDamage(Collision2D collision)
    {
        if (collision.gameObject.AsPlayer() is PlayerRef player)
        {
            var playerHealth = player.health;
            playerHealth.TakeDamage(1);
            return;
        }
    }

    private void OnTakeDamage(int damage)
    {
        stun += stunTime;
    }

    private void OnDie()
    {
        StartCoroutine(_OnDie());
    }

    private IEnumerator _OnDie()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }

    public float GetMovement()
    {
        if (!_ground.onGround) return 0;

        var groundCollider = _ground.ground.GetComponent<Collider2D>();
        float distance = Mathf.Sqrt(Mathf.Pow(_player.transform.position.x - transform.position.x,2)+ Mathf.Pow(_player.transform.position.y - transform.position.y, 2));
        if (distance < 4)
        {
            return (_player.transform.position.x < transform.position.x ? 1 : -1) * _moveSpeed * Time.deltaTime;
        }
        return 0;
    }
}
