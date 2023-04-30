using InputData;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public Item SelectedItem;

    [SerializeField] private Item _holdedItem;
    [SerializeField] private TurnHandler _turnHandler;

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
            _holdedItem.transform.localPosition = _turnHandler.ItemPlace;
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
        if (SelectedItem != null)
        {
            SelectedItem.PickUp(transform);

            _holdedItem = SelectedItem;

            SelectedItem = null;
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