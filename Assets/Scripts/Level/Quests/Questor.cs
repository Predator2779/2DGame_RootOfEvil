using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Questor : MonoBehaviour
{
    [Header("Questor")]
    public string questorName;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _smileNPC;
    [SerializeField] private Sprite _sadNPC;

    [Header("EvilLevel")]
    [SerializeField] private EvilLevel _evilLevel;///

    [Header("Dialogue")]
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private string _textGreeting = "Приветствую!";

    [Header("State Questor")]
    [SerializeField] private QuestorStates _currentState;
    private enum QuestorStates
    { Greeting, GiveQuest, NoDoneQuest, PassedQuest, DoneQuest };

    [Header("Quest")]
    [SerializeField] private Quest _currentQuest;
    [SerializeField] private Quest[] _quests;

    public Quest CurrentQuest { get { return _currentQuest; } }

    private void Start()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.sprite = _sadNPC;
        _currentState = QuestorStates.Greeting;
    }

    public void CompleteAction()
    {
        _currentQuest.countQuestAction--;

        if (_currentQuest.countQuestAction <= 0)
        {
            _currentQuest.countQuestAction = 0;

            _currentState = QuestorStates.DoneQuest;
        }
    }

    private void IsGivingQuest()
    {
        InitializeQuest(QuestAvailability());

        if (_currentQuest != null)
        {
            _currentState = QuestorStates.GiveQuest;
        }
    }

    private void InitializeQuest(Quest quest)
    {
        _currentQuest = quest;

        if (_currentQuest != null && _currentQuest.additionalQuest != null)
        {
            _currentQuest.additionalQuest.prevQuest = _currentQuest;
        }
    }

    private Quest QuestAvailability()
    {
        foreach (var quest in _quests)
        {
            if (quest.isActive 
                && _evilLevel.GetCurrentEvilLevel() <= quest.evilLevelQuestAvailability)
            {
                return quest;
            }
        }

        return null;
    }

    private void ChangeState(QuestorStates currentState)
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
                DoneQuest();
                break;
            default:
                break;
        }
    }

    private bool AdditionalQuestIsActive()
    {
        if (_currentQuest.additionalQuest != null && _currentQuest.additionalQuest.isActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Dialogue(string text)
    {
        _dialogText.text = text;
    }

    #region States

    private void Greeting()
    {
        if (_textGreeting != null)
        {
            Dialogue(_textGreeting);
        }

        IsGivingQuest();
    }

    private void GiveQuest()
    {
        EventHandler.OnQuestStart?.Invoke(_currentQuest.questName);

        var item = _currentQuest.questItem;

        Dialogue(_currentQuest.textGivingQuest + $"\n[{item.nameItem}{_currentQuest.endingPluralWord}: {_currentQuest.countQuestAction}]");

        _currentState = QuestorStates.NoDoneQuest;
    }

    private void NoDoneQuest()
    {
        var item = _currentQuest.questItem;

        Dialogue(
            _currentQuest.textNoDoneQuest +
            $"\n[{item.nameItem}{_currentQuest.endingPluralWord}: {_currentQuest.countQuestAction}]");
    }

    private void DoneQuest()
    {
        if (!AdditionalQuestIsActive())
        {
            EventHandler.OnQuestPassed?.Invoke();

            _spriteRenderer.sprite = _smileNPC;
            _dialogText.text = _currentQuest.textDoneQuest;

            _currentQuest.isActive = false;

            _currentState = QuestorStates.Greeting;
            _currentQuest = null;
        }
    }

    #endregion

    #region Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            ChangeState(_currentState);

            _dialogBox.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            _dialogBox.gameObject.SetActive(false);
        }
    }

    #endregion
}