using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
   public class SceneLoader : MonoBehaviour
   {
      private AsyncOperation? _scene;
   
      public void LoadScene(string sceneName)
      {
         _scene = SceneManager.LoadSceneAsync(sceneName);
      }
   }
}
