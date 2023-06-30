using UnityEngine;

public class Item : MonoBehaviour
{
    public string nameItem;
    public bool isNotTaken;

    public virtual void Start()
    {
        isNotTaken = true;
    }

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