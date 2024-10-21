using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateMove : MonoBehaviour
{
    [SerializeField] private string _moveParameterName = "Speed";
    [SerializeField] private string _jumpParameterName = "Jump";
    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private string _fallParameterName = "IsFalling";

    private Animator _animator;
    private IMovementController _moveController;
    private IJumpController _jumpController;
    private IAttackController _attackController;
    private Ground _ground;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveController = GetComponent<IMovementController>();
        _attackController = GetComponent<IAttackController>();
        _jumpController = GetComponent<IJumpController>();
        _ground = GetComponent<Ground>();
    }

    private void OnEnable()
    {
        if (_jumpController != null)
        {
            _jumpController.OnJump += OnJump;
        }
        if (_attackController != null)
        {
            _attackController.OnAttack += OnAttack;
        }
    }

    private void OnDisable()
    {
        if (_jumpController != null)
        {
            _jumpController.OnJump -= OnJump;
        }
        if (_attackController != null)
        {
            _attackController.OnAttack -= OnAttack;
        }
    }

    private void Update()
    {
        if (_moveController != null)
        {
            float xMovement = _moveController.GetMovement();
            _animator.SetFloat(_moveParameterName, Mathf.Abs(xMovement));
        }
        if (_ground != null)
        {
            _animator.SetBool(_fallParameterName, !_ground.onGround);
        }
    }

    private void OnJump()
    {
        _animator.SetTrigger(_jumpParameterName);
    }

    private void OnAttack()
    {
        _animator.SetTrigger(_attackParameterName);
    }
}
