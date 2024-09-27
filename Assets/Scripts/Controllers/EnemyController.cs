using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour, IMovementController
{
    // Start is called before the first frame update
    private bool _canMove;

    public float GetMovement()
    {
        if (_canMove)
        {
            return (transform.forward.x > 0) ? 1 : -1;
        }
        else
        {
            return 0;
        }
        
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.localRotation = Quaternion.Euler(0, ((transform.localRotation.y + 180) % 360), 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _canMove = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _canMove = false;
    }
}
