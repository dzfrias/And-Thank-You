using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IMovementController
{
    public float GetMovement()
    {
        return 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(_Wait(.1f));
    }

    private IEnumerator _Wait(float count)
    {
        yield return new WaitForSeconds(count);
        Destroy(gameObject);
    }
}
