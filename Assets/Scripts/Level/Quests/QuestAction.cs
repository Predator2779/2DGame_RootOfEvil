using UnityEngine;

public class QuestAction : MonoBehaviour, IUsable
{
    [SerializeField] private Questor _questor;
    [SerializeField] private Sprite _newSprite;
    [SerializeField] private bool _isPassiveItem;

    public bool isPassiveItem { get { return _isPassiveItem; } }

    private string _nameQuestItem;

    private void Awake()
    {
        _nameQuestItem = _questor.questItem.nameItem;
    }

    public void PassiveAction()
    {
        if (_isPassiveItem)
        {
            CompleteAction();
        }
    }
    
    public void ResponseAction(UsedItem item)
    {
        if (!_isPassiveItem && CheckItem(item))
        {
            CompleteAction();
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
        if (!_questor.questIsDone)
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