using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _isNotTaken;

    public string nameItem;

    private Collider2D _trigger;
    private ItemHandler _itemHandler;

    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();

        _isNotTaken = true;
    }

    public virtual void Use()
    {

    }

    public void PickUp(Transform parent)
    {
        if (_isNotTaken)
        {
            _trigger.isTrigger = false;

            transform.SetParent(parent.transform);

            _isNotTaken = false;
        }
    }

    public void Put()
    {
        if (!_isNotTaken)
        {
            transform.SetParent(null);

            _trigger.isTrigger = true;

            _isNotTaken = true;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _itemHandler))
        {
            _itemHandler.selectedItem = this;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _itemHandler))
        {
            _itemHandler.selectedItem = null;
        }
    }
}