using InputData;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _itemHandler))
        {
            _itemHandler.SelectedItem = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _itemHandler))
        {
            _itemHandler.SelectedItem = null;
        }
    }
}