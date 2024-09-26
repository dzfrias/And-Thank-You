using UnityEngine;

[RequireComponent(typeof(ShootProjectileController))]
public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 spawnLocation;
    public Quaternion rotation;

    private ShootProjectileController _controller;
    private void Awake()
    {
        _controller = GetComponent<ShootProjectileController>();
    }
    private void OnEnable()
    {
        _controller.OnFire += FireAction;
    }

    private void OnDisable()
    {
        _controller.OnFire -= FireAction;
    }
    void FireAction()
    {
        Instantiate(projectile,spawnLocation + transform.position,rotation);
    }
}
