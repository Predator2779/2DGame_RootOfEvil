using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _isNotTaken;

    private Collider2D _collider;
    private ItemHandler _itemHandler;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        _isNotTaken = true;
    }

    public virtual void Use()
    {

    }

    public void PickUp(Transform parent)
    {
        if (_isNotTaken)
        {
            _collider.isTrigger = false;

            transform.SetParent(parent.transform);

            _isNotTaken = false;
        }
    }

    public void Put()
    {
        if (!_isNotTaken)
        {
            transform.SetParent(null);

            _collider.isTrigger = true;

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