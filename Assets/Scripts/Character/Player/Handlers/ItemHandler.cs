using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    [Header("Item:")]
    public Item selectedItem;
    public Item holdedItem;
    public IUsable usableObject;

    private int _oldItemSortingOrder;

    public void PickUpItem()
    {
        if (selectedItem != null)
        {
            selectedItem.PickUp(transform);

            holdedItem = selectedItem;

            _oldItemSortingOrder = holdedItem.GetComponent<SpriteRenderer>().sortingOrder;
            holdedItem.GetComponent<SpriteRenderer>().sortingOrder = _playerSpriteRenderer.sortingOrder + 1;

            selectedItem = null;
        }
    }

    public void PutItem()
    {
        if (holdedItem != null)
        {
            holdedItem.GetComponent<SpriteRenderer>().sortingOrder = _oldItemSortingOrder;

            holdedItem.Put();

            holdedItem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            selectedItem = item;
        }

        if (collision.TryGetComponent(out IUsable usable))
        {
            usableObject = usable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item) && item == selectedItem)
        {
            selectedItem = null;
        }

        if (collision.TryGetComponent(out IUsable usable) && usable == usableObject)
        {
            usableObject = null;
        }
    }
}