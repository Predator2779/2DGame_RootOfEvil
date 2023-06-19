using UnityEngine.UI;
using UnityEngine;
using TMPro;
using InputData;

public class Questor : MonoBehaviour
{
    [Header("Questor")]
    public string questorName;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _smileNPC;
    [SerializeField] private Sprite _sadNPC;

    [Header("EvilLevel")]
    [SerializeField] private EvilLevelCounter _evilLevelCounter;

    [Header("Dialogue")]
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [TextArea(2, 4)]
    [SerializeField] private string _textGreeting = "Приветствую!";

    [Header("Quest")]
    [SerializeField] private Quest[] _quests;

    [SerializeField] private bool _dialogueReady = false;

    private GameModes _gameMode;

    private void Start()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.sprite = _sadNPC;
    }

    private void Update()////////
    {
        if (InputFunctions.GetKeyF_Up() && _dialogueReady)
        {
            Dialogue();
        }
    }

    private void OnEnable()
    {
        EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);
    }

    private void OnDisable()
    {
        EventHandler.OnGameModeChanged.RemoveListener(ChangeGameMode);
    }

    private void ChangeGameMode(GameModes mode)
    {
        _gameMode = mode;
    }

    public void Greeting()
    {
        Say("[F] - поговорить.");
    }

    public void Say(string text)
    {
        _dialogText.text = text;
        _dialogBox.gameObject.SetActive(true);
        _dialogueReady = true;
    }

    public void Dialogue()
    {
        _dialogBox.gameObject.SetActive(false);

        EventHandler.OnDialogueWindowShow?.Invoke(true);
        EventHandler.OnShowQuests?.Invoke
            (_quests, this, _evilLevelCounter.GetCurrentEvilLevel());//////////////////////////////////////
        EventHandler.OnReplicaSay?.Invoke(_textGreeting);
        EventHandler.OnGameModeChanged?.Invoke(GameModes.Pause);
    }

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _smileNPC;
    }

    #region Trigger

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
        {
            Greeting();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
        {
            _dialogueReady = false;
            _dialogBox.gameObject.SetActive(false);
        }
    }

    #endregion
}