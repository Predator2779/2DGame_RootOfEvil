using UnityEngine;

public class QuestAction : MonoBehaviour, IUsable
{
    [SerializeField] private ItemQuest _quest;
    [SerializeField] private Sprite _newSprite;
    [SerializeField] private bool _isPassiveItem = true;
    [SerializeField] private bool _isReusable = false;

    public bool isPassiveItem { get { return _isPassiveItem; } }
    
    private string _nameQuestItem;

    private void Start()
    {
        EventHandler.OnQuestStart.AddListener(CheckStartingQuest);
    }

    public void CheckStartingQuest(Quest quest)
    {
        if (quest == _quest)
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
                gameObject.SetActive(false);
        }
    }

    public bool CheckItem(Item item)
    {
        if (_nameQuestItem != null && item.nameItem == _nameQuestItem)
            return true;
        else
            return false;
    }

    public void CompleteAction()
    {
        _quest.CompleteAction();

        if (_newSprite != null)
            GetComponent<SpriteRenderer>().sprite = _newSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item) && CheckItem(item))
            PassiveAction();
    }

    #region NotImplemented

    public void SecondaryAction()
    {
        throw new System.NotImplementedException();
    }

    public void PrimaryAction(IUsable usable)
    {
        throw new System.NotImplementedException();
    }

    #endregion
}