using UnityEngine;

public class Item : MonoBehaviour
{
    public string nameItem;
    private bool _isNotTaken;

    public virtual void Start()
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