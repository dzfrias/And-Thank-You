using UnityEngine;

[RequireComponent(typeof(IAttackController))]
public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 spawnLocation;
    public Quaternion rotation;

    private IAttackController _controller;

    private void Awake()
    {
        _controller = GetComponent<IAttackController>();
    }

    private void OnEnable()
    {
        _controller.OnAttack += FireAction;
    }

    private void OnDisable()
    {
        _controller.OnAttack -= FireAction;
    }

    void FireAction()
    {
        Instantiate(projectile,spawnLocation + transform.position,rotation);
    }
}
