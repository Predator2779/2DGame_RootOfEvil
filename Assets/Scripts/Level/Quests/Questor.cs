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
    [SerializeField] private Quest _optionalQuest;//non
    [SerializeField] private Quest[] _quests;

    private GameModes _gameMode;

    private void Start()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.sprite = _sadNPC;
    }

    private void OnEnable()
    {
        EventHandler.OnOptionalQuest.AddListener(SetOptionalQuest);
        EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);
    }

    private void OnDisable()
    {
        EventHandler.OnOptionalQuest.RemoveListener(SetOptionalQuest);
        EventHandler.OnGameModeChanged.RemoveListener(ChangeGameMode);
    }

    private void ChangeGameMode(GameModes mode)
    {
        _gameMode = mode;
    }

    public void Greeting()
    {
        if (_textGreeting != null)
        {
            Say(_textGreeting + "\n\n[F] - поговорить.");
        }
    }

    public void Say(string text)
    {
        _dialogText.text = text;
        _dialogBox.gameObject.SetActive(true);
    }

    public void Dialogue()
    {
        if (InputFunctions.GetKeyF())
        {
            EventHandler.OnDialogueWindowShow?.Invoke(true);
            EventHandler.OnShowQuests?.Invoke(_quests, this);//////////////////////////////////////
            EventHandler.OnGameModeChanged?.Invoke(GameModes.Pause);
        }
    }

    //public void CheckQuest()
    //{
    //    if (_optionalQuest != null)
    //    {
    //        _optionalQuest.CheckQuest();

    //        return;
    //    }

    //    foreach (var quest in _quests)
    //    {
    //        if (quest.QuestAvailability(this, _evilLevelCounter.GetCurrentEvilLevel()))
    //        {
    //            quest.CheckQuest();

    //            return;
    //        }
    //    }

    //    Greeting();
    //}

    public void ChangeSprite()
    {
        _spriteRenderer.sprite = _smileNPC;
    }

    public void SetOptionalQuest(Quest optionalQuest)
    {
        foreach (var quest in _quests)
        {
            if (quest == optionalQuest)
            {
                _optionalQuest = optionalQuest;
            }
        }
    }

    #region Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
        {
            Greeting();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
        {
            Dialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
        {
            _dialogBox.gameObject.SetActive(false);
        }
    }

    #endregion
}