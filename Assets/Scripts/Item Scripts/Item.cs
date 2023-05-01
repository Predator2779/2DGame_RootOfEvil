using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _isNotTaken;

    public string nameItem;

    private void Awake()
    {
        _isNotTaken = true;
    }

    public virtual void Use(IUsable usable)
    {
        usable.CheckUsing(this);
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