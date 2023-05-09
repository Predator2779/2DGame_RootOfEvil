using InputData;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Item _holdedItem;
    [SerializeField] private TurnHandler _turnHandler;

    public Item selectedItem;

    private int _itemSortingOrder;
    private int _oldItemSortingOrder;

    private void Start()
    {
        _turnHandler = GetComponent<TurnHandler>();
        _itemSortingOrder = transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder + 1;
    }

    private void FixedUpdate()
    {
        Carrying();
    }

    private void Carrying()
    {
        if (_holdedItem != null)
        {
            _holdedItem.transform.localPosition = _turnHandler.itemPlace;
        }
    }

    public void UseAnObject(IUsable usable)
    {
        if (_holdedItem != null)
        {
            _holdedItem.Use(usable);
        }
    }

    public void PickUpItem()
    {
        if (selectedItem != null)
        {
            selectedItem.PickUp(transform);

            _holdedItem = selectedItem;

            _oldItemSortingOrder = _holdedItem.GetComponent<SpriteRenderer>().sortingOrder;
            _holdedItem.GetComponent<SpriteRenderer>().sortingOrder = _itemSortingOrder;

            selectedItem = null;
        }
    }

    public void PutItem()
    {
        if (_holdedItem != null)
        {
            _holdedItem.GetComponent<SpriteRenderer>().sortingOrder = _oldItemSortingOrder;

            _holdedItem.Put();

            _holdedItem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item>(out Item item))
        {
            selectedItem = item;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item>(out Item item))
        {
            selectedItem = null;
        }
    }
}