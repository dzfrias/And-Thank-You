using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public GameObject parent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("player"))
        {
            StartCoroutine(OnDie());
        }
    }

    IEnumerator OnDie()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(parent);
    }
}
