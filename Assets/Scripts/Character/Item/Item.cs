using UnityEngine;

public class Item : MonoBehaviour
{
    private bool _isNotTaken = true;

    public bool isNotTaken { get { return _isNotTaken; }  set { _isNotTaken = value; } }
    public string nameItem;

    public void PickUp(Transform parent)
    {
        if (isNotTaken)
        {
            transform.SetParent(parent.transform);

            isNotTaken = false;
        }
    }

    public void Put()
    {
        if (!isNotTaken)
        {
            transform.SetParent(null);

            isNotTaken = true;
        }
    }
}