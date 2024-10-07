using UnityEngine;
using UnityEngine.SceneManagement;

public struct PlayerRef
{
    internal PlayerRef(Health health, Rigidbody2D rigidbody, Collider2D collider, Transform transform, Direction direction)
    {
        this.health = health;
        this.rigidbody = rigidbody;
        this.collider = collider;
        this.transform = transform;
        this.direction = direction;
    }

    public Health health { get; }
    public Rigidbody2D rigidbody { get; }
    public Collider2D collider { get; }
    public Transform transform { get; }
    public Direction direction { get; }
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
        var direction = gameObject.GetComponent<Direction>();
        _cache = new PlayerRef(health, rigidbody, collider, gameObject.transform,direction);
        SceneManager.sceneLoaded += OnSceneLoaded;
        return _cache;
    }

    public static PlayerRef Player(this GameObject gameObject)
    {
        if (_cache is PlayerRef ref_) return ref_;
        var player = GameObject.FindWithTag("Player");
        var health = player.GetComponent<Health>();
        var rigidbody = player.GetComponent<Rigidbody2D>();
        var collider = player.GetComponent<Collider2D>();
        var direction = player.GetComponent<Direction>();
        var newRef = new PlayerRef(health, rigidbody, collider, player.transform, direction);
        _cache = newRef;
        SceneManager.sceneLoaded += OnSceneLoaded;
        return newRef;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // We need to refresh the cache when a new scene loads. This is because
        // new objects are created when a new scene loads (the old PlayerRef 
        // data should be garbage collected). Holding on to our old PlayerRef
        // not only creates memory leaks, but also makes any reference to our 
        // player go to an object that doesn't exist in our scene (it goes to 
        // the old one).
        _cache = null;
    }
}
