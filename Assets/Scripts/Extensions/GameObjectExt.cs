using UnityEngine;

public struct PlayerRef
{
    internal PlayerRef(Health health, Rigidbody2D rigidbody, Collider2D collider, Transform transform)
    {
        this.health = health;
        this.rigidbody = rigidbody;
        this.collider = collider;
        this.transform = transform;
    }

    public Health health { get; }
    public Rigidbody2D rigidbody { get; }
    public Collider2D collider { get; }
    public Transform transform { get; }
}

public static class GameObjectExt
{
    private static PlayerRef? _cache;

    public static PlayerRef? AsPlayer(this GameObject gameObject)
    {
        if (!gameObject.CompareTag("Player")) return null;
        if (_cache is not null) return _cache;
        var health = gameObject.GetComponent<Health>();
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();
        var collider = gameObject.GetComponent<Collider2D>();
        _cache = new PlayerRef(health, rigidbody, collider, gameObject.transform);
        return _cache;
    }
}
