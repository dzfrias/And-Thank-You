using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private int _sceneNumber;
    
    public void SwitchScene()
    {
        SceneManager.LoadScene(_sceneNumber);
    }
}
