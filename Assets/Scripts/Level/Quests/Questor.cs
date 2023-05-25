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
    [SerializeField] private EvilLevelCounter _evilLevelCounter;

    [Header("Dialogue")]
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private string _textGreeting = "Приветствую!";

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
    }

    public void Greeting()
    {
        if (_textGreeting != null)
        {
            Dialogue(_textGreeting);
        }
    }

    public void Dialogue(string text)
    {
        _dialogText.text = text;
        _dialogBox.gameObject.SetActive(true);
    }

    public void CompleteAction()
    {
        if (_currentQuest != null)
        {
            _currentQuest.CompleteAction();
        }
    }

    public void CheckQuest()
    {
        if (_currentQuest != null)
        {
            _currentQuest.CheckQuest();
        }
        else
        {
            Greeting();

            InitializeQuest();
        }
    }

    public void InitializeQuest()
    {
        foreach (var quest in _quests)
        {
            if (quest.QuestAvailability(this, _evilLevelCounter.GetCurrentEvilLevel()))
            {
                _currentQuest = quest;
            }
        }
    }

    public void PassedQuest()
    {
        EventHandler.OnQuestPassed?.Invoke();

        _spriteRenderer.sprite = _smileNPC;

        _currentQuest = null;
    }

    #region Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            CheckQuest();
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