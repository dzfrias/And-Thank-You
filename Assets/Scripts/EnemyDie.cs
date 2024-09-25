using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("player"))
        {
            StartCoroutine(onDie());
        }
    }

    IEnumerator onDie()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(parent);
    }
}
