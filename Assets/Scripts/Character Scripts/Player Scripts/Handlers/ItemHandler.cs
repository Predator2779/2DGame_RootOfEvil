using InputData;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Item _holdedItem;
    [SerializeField] private TurnHandler _turnHandler;

    public Item selectedItem;

    private void Start()
    {
        _turnHandler = GetComponent<TurnHandler>();
    }

    private void Update()
    {
        GetE();
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

    private void GetE()
    {
        if (InputFunctions.GetKeyE())
        {
            PutItem();

            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        if (selectedItem != null)
        {
            selectedItem.PickUp(transform);

            _holdedItem = selectedItem;

            selectedItem = null;
        }
    }

    private void PutItem()
    {
        if (_holdedItem != null)
        {
            _holdedItem.Put();

            _holdedItem = null;
        }
    }
}