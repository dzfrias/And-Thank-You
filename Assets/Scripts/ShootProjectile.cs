using UnityEngine;

[RequireComponent(typeof(IAttackController))]
public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Vector3 spawnLocation;
    public Quaternion rotation;

    private IAttackController _controller;
    private Direction _direction;

    private void Start()
    {
        _direction = GetComponent<Direction>();
    }

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
        Instantiate(projectile, spawnLocation + transform.position, _direction.IsRight() ? Quaternion.identity : new Quaternion(0, 1, 0, 180));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Vector3 offset = spawnLocation;
        Gizmos.DrawCube(transform.position + offset, new Vector2(0.5f, 0.5f));
    }
}
