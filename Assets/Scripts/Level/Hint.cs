using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [TextArea(2, 4)]
    [SerializeField] private string _textHint;
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;

    private GameModes _gameMode;

    private void Start() => EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);

    private void ChangeGameMode(GameModes mode) => _gameMode = mode;

    public void HintSetActive(bool value, string text = "")
    {
        _dialogText.text = text;
        _dialogBox.gameObject.SetActive(value);
    }

    #region Trigger

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
            HintSetActive(true, _textHint);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
            HintSetActive(false);
    }

    #endregion
}