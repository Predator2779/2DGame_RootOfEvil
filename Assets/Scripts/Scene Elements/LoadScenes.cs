using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadScenePressed(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}