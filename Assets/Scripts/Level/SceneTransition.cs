using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string _transitionScene;

    private bool _isReady = false;

    private GameModes _gameMode;

    private void Start() => EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);

    private void Update()
    {
        if (_isReady && InputData.InputFunctions.GetKeyF_Up())
            SceneManager.LoadScene(_transitionScene);
    }

    private void ChangeGameMode(GameModes mode) => _gameMode = mode;

    #region Trigger

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (
            _gameMode == GameModes.Playing &&
            collision.transform.tag == "Player"
            )
            _isReady = true;
    } 
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (
            _gameMode == GameModes.Playing &&
            collision.transform.tag == "Player"
            )
            _isReady = false;
    }

    #endregion
}