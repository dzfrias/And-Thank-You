using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GunEnemyController : MonoBehaviour, IMovementController, IAttackController
{
    [SerializeField] private float _runAwayDistance = 4f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _stunTime = 0.2f;

    private Ground _ground;
    private Collider2D _collider;
    private Health _health;
    private float _stun;
    private float _attackCooldown;
    private PlayerRef _player;

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
            if (_stun > 0) continue;
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
        _stun += _stunTime;
    }

    private void Update()
    {
        _stun = Mathf.Max(_stun - Time.deltaTime, 0f);
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
        if (!_ground.onGround || _stun > 0) return 0;

        var groundCollider = _ground.ground.GetComponent<Collider2D>();
        float distance = Mathf.Abs(_player.transform.position.x - transform.position.x);
        if (distance < _runAwayDistance)
        {
            return (_player.transform.position.x < transform.position.x ? 1 : -1);
        }
        return 0;
    }
}
