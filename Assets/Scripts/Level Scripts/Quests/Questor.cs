using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Questor : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private Sprite _smileNPC;
    [SerializeField] private Sprite _sadNPC;

    [Header("Dialogue")]
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;

    [SerializeField] private string _textGreeting = "Приветствую!";
    [SerializeField] private string _textGiveQuest = 
        "Тебе нужно отрубить все мизинцы на пальцах ног, в этой деревне!";
    [SerializeField] private string _textNoDoneQuest = "Прошу, отруби все мизинцы на пальцах ног в деревне!";
    [SerializeField] private string _textDoneQuest = "Ты отрубил все проклятые мизинцы! Поздравляю!";
    [SerializeField] private string _endingPluralWord = "ов";

    [Header("Counts")]
    [SerializeField] private int _countQuestAction;

    [Header("Booleans")]
    [SerializeField] private bool _isAccepted = false;
    [SerializeField] private bool _isDone = false;

    public Item questItem;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sadNPC;
    }

    public void CompleteAction()
    {
        _countQuestAction--;

        if (_countQuestAction <= 0)
        {
            _isDone = true;
        }
    }

    private void CheckCompleteQuest()
    {
        if (!_isAccepted && !_isDone)
        {
            GiveQuest();
        }
        else if (_isAccepted && !_isDone)
        {
            Dialogue(_textNoDoneQuest + 
                $" [{questItem.nameItem}{_endingPluralWord}: {_countQuestAction}]");
        }
        else if (_isAccepted && _isDone)
        {
            PassQuest();
        }
        else
        {
            Dialogue(_textGreeting);
        }
    }

    private void GiveQuest()
    {
        _dialogText.text = _textGiveQuest + 
            $" [{questItem.nameItem}{_endingPluralWord}: {_countQuestAction}]";
        _isAccepted = true;
    }

    private void PassQuest()
    {
        EventHandler.OnQuestPassed?.Invoke();

        _spriteRenderer.sprite = _smileNPC;
        _dialogText.text = _textDoneQuest;
        _isDone = true;
    }


    private void Dialogue(string text)
    {
        _dialogText.text = text;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            _dialogBox.gameObject.SetActive(true);

            CheckCompleteQuest();
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