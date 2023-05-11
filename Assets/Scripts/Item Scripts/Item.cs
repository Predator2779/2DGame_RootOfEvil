using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _isNotTaken;

    public string nameItem;

    public virtual void Awake()
    {
        _isNotTaken = true;
    }

    public void PickUp(Transform parent)
    {
        if (_isNotTaken)
        {
            transform.SetParent(parent.transform);

            _isNotTaken = false;
        }
    }

    public void Put()
    {
        if (!_isNotTaken)
        {
            transform.SetParent(null);

            _isNotTaken = true;
        }
    }
}