using System.Collections.Generic;
using UnityEngine;

public class UsedItem : Item
{
    [Tooltip("Одноразовые предметы исчезают после использования")]
    public bool _oneUse = false;

    private List<UsedItem> usedItems;

    public virtual void PrimaryAction()
    {
        foreach (UsedItem item in usedItems)
            item.ResponseAction(this);

        if (_oneUse)
            Destroy(gameObject);
    }

    public virtual void AddToList(Collider2D collision)
    {
        if (collision.TryGetComponent(out UsedItem usedItem))
            if (!usedItems.Contains(usedItem))
                usedItems.Add(usedItem);
    }

    public virtual void RemoveFromList(Collider2D collision)
    {
        if (collision.TryGetComponent(out UsedItem usedItem))
            if (usedItems.Contains(usedItem))
                usedItems.Remove(usedItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddToList(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveFromList(collision);
    }

    #region Not Implemented

    public virtual void SecondaryAction()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ResponseAction(UsedItem item)
    {
        throw new System.NotImplementedException();
    }

    public virtual void PassiveAction()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}