using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IMovementController))]
public class MoveConstantSpeed : MonoBehaviour
{
    private IMovementController _controller;
    private void Awake()
    {
        _controller = GetComponent<IMovementController>();
    }

    private void Update()
    {
        transform.Translate(new Vector3(_controller.GetMovement() * Time.deltaTime,0,0));
    }
}
