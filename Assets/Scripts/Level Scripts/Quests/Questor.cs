using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Questor : MonoBehaviour
{
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;

    [SerializeField] private string _textGreeting = "�����������!";
    [SerializeField] private string _textGiveQuest = 
        "���� ����� �������� ��� ������� �� ������� ���, � ���� �������!";
    [SerializeField] private string _textNoDoneQuest = "�����, ������ ��� ������� �� ������� ��� � �������!";
    [SerializeField] private string _textDoneQuest = "�� ������� ��� ��������� �������! ����������!";
    [SerializeField] private string _endingPluralWord = "��";

    [SerializeField] private int _countQuestAction;

    [SerializeField] private bool _isAccepted = false;
    [SerializeField] private bool _isDone = false;

    public Item questItem;

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
        /// ����� ���� -1, ��� ��� ���������� �������� ��������� ������ ���.
        EventHandler.OnEvilLevelChanged?.Invoke(-1);

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