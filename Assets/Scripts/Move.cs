using UnityEngine;

[RequireComponent(typeof(MovementController), typeof(Ground), typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 100f)] private float _maxDeceleration = 50f;
    [SerializeField, Range(0f, 100f)] private float _maxAirDeceleration = 40f;

    private MovementController _controller;
    private Vector2 _desiredVelocity;
    private Vector2 _velocity;
    private Rigidbody2D _body;
    private Ground _ground;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<MovementController>();
    }

    private void Update()
    {
        float xMovement = _controller.GetMovement();
        _desiredVelocity = new Vector2(xMovement, 0f) * Mathf.Max(_maxSpeed - _ground.friction, 0f);
    }

    private void FixedUpdate()
    {
        float acceleration;
        if (_controller.GetMovement() == 0)
        {
            acceleration = _ground.onGround ? _maxDeceleration : _maxAirDeceleration;
        }
        else
        {
            acceleration = _ground.onGround ? _maxAcceleration : _maxAirAcceleration;
        }
        float xVelocity = Mathf.MoveTowards(_body.velocity.x, _desiredVelocity.x, acceleration * Time.fixedDeltaTime);
        _body.velocity = new Vector2(xVelocity, _body.velocity.y);
    }
}
