using UnityEngine;

public class QuestAction : MonoBehaviour, IUsable
{
    [SerializeField] private Questor _questor;
    [SerializeField] private Sprite _newSprite;
    [SerializeField] private bool _isUsable = false;

    private string _nameQuestItem;

    private void Awake()
    {
        _nameQuestItem = _questor.questItem.nameItem;
    }

    public void PerformAction()
    {
        if (!_isUsable)
        {
            CompleteAction();
        }
    }
    
    public void ApplyItem(UsedItem item)
    {
        if (_isUsable && CheckItem(item))
        {
            CompleteAction();
        }
    }

    private bool CheckItem(Item item)
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
            PerformAction();
        }
    }
}