using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private TurnHandler _turnHandler;
    [SerializeField] private IUsable _usableObj;

    public Item holdedItem;
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

    public void UseItem()
    {
        if (CheckUsing() && holdedItem.TryGetComponent(out UsedItem usedItem))
        {
            usedItem.Use(_usableObj);
        }
    }

    public void PickUpItem()
    {
        if (selectedItem != null)
        {
            selectedItem.PickUp(transform);

            holdedItem = selectedItem;

            _oldItemSortingOrder = holdedItem.GetComponent<SpriteRenderer>().sortingOrder;
            holdedItem.GetComponent<SpriteRenderer>().sortingOrder = _itemSortingOrder;

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

    private void Carrying()
    {
        //if (holdedItem != null)
        //{
        //    holdedItem.transform.localPosition = _turnHandler.itemPlace;
        //}
    }

    private bool CheckUsing()
    {
        if (holdedItem != null && _usableObj != null)
        {
            return true;
        }
        else
        {
            return false;
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
            _usableObj = usable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            selectedItem = null;
        }

        if (collision.TryGetComponent(out IUsable usable))
        {
            _usableObj = null;
        }
    }
}