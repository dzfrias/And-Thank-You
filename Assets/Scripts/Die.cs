using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("playerDies"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
