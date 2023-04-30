using UnityEngine;
using InputData;

public class Item : MonoBehaviour
{
    private BoxCollider2D _collider;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private bool _isNotTaken = true;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();

        _collider.enabled = _isNotTaken;
    }

    private void Update()
    {
        Using();
    }

    private void FixedUpdate()
    {
        Carrying();
    }

    private void Carrying()
    {
        if (_turnController != null && !_isNotTaken)
        {
            transform.localPosition = _turnController.ItemPlace;
        }
    }

    private void Using()
    {
        if (_turnController != null && InputFunctions.GetKeyE())
        {
            if (_isNotTaken)
            {
                PickUp();
            }
            else
            {
                Put();
            }
        }
    }

    private void PickUp()
    {
        _collider.isTrigger = false;

        transform.SetParent(_turnController.transform);

        _isNotTaken = false;
    }

    private void Put()
    {
        if (InputFunctions.GetKeyE())
        {
            transform.SetParent(null);

            _collider.isTrigger = true;

            _isNotTaken = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out _turnController);
    }
}