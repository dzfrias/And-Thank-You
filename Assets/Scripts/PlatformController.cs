using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatformController : MonoBehaviour, IMovementController
{
    [SerializeField, Range(0f, 100f)] private float _rightBound = 10f;
    [SerializeField, Range(0f, 100f)] private float _leftBound = 10f;
    [SerializeField] private bool _startRight = true;

    private Collider2D _collider;
    private float _startPos;
    private float _direction;

    private void Start()
    {
        _startPos = transform.position.x;
        _collider = GetComponent<Collider2D>();
        _direction = _startRight ? 1f : -1f;
    }

    private void Update()
    {
        if (_collider.bounds.center.x + _collider.bounds.extents.x >= _startPos + _rightBound)
        {
            _direction = -1f;
        }
        if (_collider.bounds.center.x - _collider.bounds.extents.x <= _startPos - _leftBound)
        {
            _direction = 1f;
        }
    }

    public float GetMovement()
    {
        return _direction;
    }
}
