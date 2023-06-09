using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private Item selectedItem;
    private Item holdedItem;
    private IUsable usableObject;

    private int _oldItemSortingOrder;

    public Item HoldedItem { get => holdedItem; }

    public void PickUpItem()
    {
        if (selectedItem != null)
        {
            selectedItem.PickUp(transform);

            holdedItem = selectedItem;
            SetCharacterItem(holdedItem);

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
            SetCharacterItem(holdedItem);
        }
    }

    private void SetCharacterItem(Item item)
    {
        _character.holdedItem = item;
    }

    private void SetCharacterUsableObject(IUsable usable)
    {
        _character.usableObject = usable;
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
            SetCharacterUsableObject(usable);
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
            SetCharacterUsableObject(usable);
        }
    }
}