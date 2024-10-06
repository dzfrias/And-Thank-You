using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateMove : MonoBehaviour
{
    [SerializeField] private string _moveParameterName = "Speed";
    [SerializeField] private string _jumpParameterName = "IsJumping";

    private Animator _animator;
    private IMovementController _moveController;
    private IJumpController _jumpController;
    private Ground _ground;

    private bool canReturn = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveController = GetComponent<IMovementController>();

        _jumpController = GetComponent<IJumpController>();
        _ground = GetComponent<Ground>();
        if ((_jumpController != null && _ground == null) || (_jumpController == null && _ground != null))
        {
            Debug.LogError("Ground component must accompany jump controller");
        }
    }

    private void OnEnable()
    {
        if (_jumpController != null)
        {
            _jumpController.OnJump += OnJump;
        }
    }

    private void OnDisable()
    {
        if (_jumpController != null)
        {
            _jumpController.OnJump -= OnJump;
        }
    }

    private void Update()
    {
        if (_moveController != null)
        {
            float xMovement = _moveController.GetMovement();
            _animator.SetFloat(_moveParameterName, Mathf.Abs(xMovement));
        }

        if (_jumpController != null && _animator.GetBool(_jumpParameterName) && _ground.onGround && canReturn)
        {
            _animator.SetBool(_jumpParameterName, false);
        }
    }

    private void OnJump()
    {
        _animator.SetBool(_jumpParameterName, true);
        StartCoroutine(CanReturn());
    }

    private IEnumerator CanReturn()
    {
        canReturn = false;
        yield return new WaitForSeconds(0.2f);
        canReturn = true;
    }
}
