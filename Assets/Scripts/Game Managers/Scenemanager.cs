using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scenemanager : MonoBehaviour
{
    public static Scenemanager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void LoadScene(string sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }


    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }


}
