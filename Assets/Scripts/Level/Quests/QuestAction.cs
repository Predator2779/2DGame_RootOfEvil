using UnityEngine;

public class QuestAction : MonoBehaviour, IUsable
{
    [SerializeField] private Quest _quest;
    [SerializeField] private Questor _questor;
    [SerializeField] private string _nameQuestItem;
    [SerializeField] private Sprite _newSprite;
    [SerializeField] private bool _isPassiveItem = true;
    [SerializeField] private bool _isReusable = false;

    public bool isPassiveItem { get { return _isPassiveItem; } }

    private void Start()
    {
        EventHandler.OnQuestStart.AddListener(StartQuest);
    }

    public void StartQuest(string questName)
    {
        if (questName == _quest.questName)
        {
            _nameQuestItem = _quest.questItem.nameItem;
            _isPassiveItem = true;
        }
    }

    public void PassiveAction()
    {
        if (isPassiveItem)
        {
            CompleteAction();

            gameObject.SetActive(false);
        }
    }
    
    public void ResponseAction(UsedItem item)
    {
        if (!isPassiveItem && CheckItem(item))
        {
            CompleteAction();

            if (!_isReusable)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public bool CheckItem(Item item)
    {
        if (_nameQuestItem != null && item.nameItem == _nameQuestItem)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CompleteAction()
    {
        if (_quest.isActive)
        {
            _questor.CompleteAction();

            if (_newSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = _newSprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item) && CheckItem(item))
        {
            PassiveAction();
        }
    }

    #region NotImplemented

    public void PrimaryAction()
    {
        throw new System.NotImplementedException();
    }

    public void SecondaryAction(IUsable usable)
    {
        throw new System.NotImplementedException();
    }

    #endregion
}