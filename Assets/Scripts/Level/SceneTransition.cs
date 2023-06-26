using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string _transitionScene;

    private GameModes _gameMode;

    private void Start() => EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);

    private void ChangeGameMode(GameModes mode) => _gameMode = mode;

    #region Trigger

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
            SceneManager.LoadScene(_transitionScene);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
            SceneManager.LoadScene(_transitionScene);
    }

    #endregion
}