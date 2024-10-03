using UnityEngine;

public static class ColliderExt
{
    public static float Right(this Collider2D collider)
    {
        return collider.bounds.center.x + collider.bounds.extents.x;
    }

    public static float Left(this Collider2D collider)
    {
        return collider.bounds.center.x - collider.bounds.extents.x;
    }

    public static float Top(this Collider2D collider)
    {
        return collider.bounds.center.y + collider.bounds.extents.y;
    }

    public static float Bottom(this Collider2D collider)
    {
        return collider.bounds.center.y - collider.bounds.extents.y;
    }
}
