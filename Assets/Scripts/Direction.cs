using UnityEngine;

public class Direction : MonoBehaviour
{
    private bool _isRight = true;

    public bool IsRight()
    {
        return _isRight;
    }

    public bool IsLeft()
    {
        return !_isRight;
    }

    public float AsSign()
    {
        return _isRight ? 1 : -1;
    }

    public void SetRight()
    {
        _isRight = true;
    }

    public void SetLeft()
    {
        _isRight = false;
    }

    public void SetBySign(float sign)
    {
        if (sign == 0) return;
        _isRight = sign > 0;
    }

    public void Flip()
    {
        _isRight = !_isRight;
    }
}
