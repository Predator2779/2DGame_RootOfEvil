using InputData;
using UnityEngine;

public class QuestAction : MonoBehaviour
{
    [SerializeField] private Questor _questor;
    [SerializeField] private Sprite _newSprite;

    private string _questItem;

    private void Awake()
    {
        _questItem = _questor.questItem.nameItem;
    }

    private void CompleteAction()
    {
        _questor.CompleteAction();

        if (_newSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = _newSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item>(out Item item))
        {
            if (_questItem != null && item.nameItem == _questItem)
            {
                CompleteAction();
            }

            if (_questItem == null && InputFunctions.GetLMB())
            {
                CompleteAction();
            }
        }
    }
}