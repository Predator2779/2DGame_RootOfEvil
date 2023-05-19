using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Questor : MonoBehaviour
{
    [Header("EvilLevel")]
    [SerializeField] private EvilLevel _evilLevel;
    [SerializeField] private int _evilLevelQuestAvailability;

    [Header("Sprites")]
    [SerializeField] private Sprite _smileNPC;
    [SerializeField] private Sprite _sadNPC;

    [Header("OtherQuest")]
    [SerializeField] private Questor _passedNPC;

    public Questor startNPC;
    public string textPassedNPC = "Спасибо, за пирожки!";

    [Header("Dialogue")]
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;

    [SerializeField] private string _textGreeting = "Приветствую!";
    [SerializeField]
    private string _textGiveQuest =
        "Тебе нужно отрубить все мизинцы на пальцах ног, в этой деревне!";
    [SerializeField] private string _textNoDoneQuest = "Прошу, отруби все мизинцы на пальцах ног в деревне!";
    [SerializeField] private string _textDoneQuest = "Ты отрубил все проклятые мизинцы! Поздравляю!";
    [SerializeField] private string _endingPluralWord = "ов";

    [Header("Count")]
    [SerializeField] private int _countQuestAction;

    [SerializeField]
    private enum QuestorStates
    { OtherQuest, Greeting, GiveQuest, NoDoneQuest, DoneQuest };

    [SerializeField] private QuestorStates _currentState;

    [Header("ItemS")]
    public Item questItem;

    [Header("Booleans")]
    public bool questIsDone;
    public bool isOtherQuest;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sadNPC;

        _currentState = QuestorStates.Greeting;

        if (_passedNPC != null)
        {
            _passedNPC.startNPC = this;
        }
    }

    private void QuestAvailability()
    {
        if (isOtherQuest)
        {
            _currentState = QuestorStates.OtherQuest;

            return;
        }

        if (IsGivingQuest())
        {
            _currentState = QuestorStates.GiveQuest;
        }
    }

    private void CheckCompleteQuest(QuestorStates currentState)
    {
        switch (currentState)
        {
            case QuestorStates.Greeting:
                Greeting();
                break;
            case QuestorStates.GiveQuest:
                GiveQuest();
                break;
            case QuestorStates.NoDoneQuest:
                NoDoneQuest();
                break;
            case QuestorStates.DoneQuest:
                PassQuest();
                break;
            case QuestorStates.OtherQuest:
                PassOtherQuest();
                break;
            default:
                break;
        }
    }

    private bool IsGivingQuest()
    {
        if (_evilLevel.GetCurrentEvilLevel() <= _evilLevelQuestAvailability
            && !questIsDone
            && _currentState == QuestorStates.Greeting)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Greeting()
    {
        Dialogue(_textGreeting);
    }

    private void Dialogue(string text)
    {
        _dialogText.text = text;
    }

    private void GiveQuest()
    {
        _dialogText.text = _textGiveQuest + $" [{questItem.nameItem}{_endingPluralWord}: {_countQuestAction}]";

        if (_passedNPC != null)
        {
            _passedNPC.isOtherQuest = true;
        }

        _currentState = QuestorStates.NoDoneQuest;
    }

    public void CompleteAction()
    {
        _countQuestAction--;

        if (_countQuestAction <= 0)
        {
            _currentState = QuestorStates.DoneQuest;
        }
    }

    private void NoDoneQuest()
    {
        Dialogue(_textNoDoneQuest + $" [{questItem.nameItem}{_endingPluralWord}: {_countQuestAction}]");
    }

    private void PassQuest()
    {
        EventHandler.OnQuestPassed?.Invoke();

        _spriteRenderer.sprite = _smileNPC;
        _dialogText.text = _textDoneQuest;

        questIsDone = true;

        _currentState = QuestorStates.Greeting;
    }

    private void PassOtherQuest()
    {
        _spriteRenderer.sprite = _smileNPC;
        _dialogText.text = startNPC.textPassedNPC;

        isOtherQuest = false;

        _currentState = QuestorStates.Greeting;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            _dialogBox.gameObject.SetActive(true);

            QuestAvailability();

            CheckCompleteQuest(_currentState);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            _dialogBox.gameObject.SetActive(false);
        }
    }
}