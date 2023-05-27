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
    [SerializeField] private Quest _optionalQuest;//non
    [SerializeField] private Quest[] _quests;

    private void Start()
    {
        EventHandler.OnOptionalQuest.AddListener(SetOptionalQuest);

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

    public void CompleteAction(Quest quest)
    {
        quest.CompleteAction();
    }

    public void CheckQuest()
    {
        if (_optionalQuest != null)
        {
            _optionalQuest.CheckQuest();

            return;
        }

        foreach (var quest in _quests)
        {
            if (quest.QuestAvailability(this, _evilLevelCounter.GetCurrentEvilLevel()))
            {
                quest.CheckQuest();

                return;
            }
        }

        Greeting();
    }

    public void PassedQuest()
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