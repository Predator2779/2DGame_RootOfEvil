using UnityEngine;

public class UsedItem : Item, IUsable
{
    [Tooltip("Одноразовые предметы исчезают после использования")]
    public bool _oneUse = false;

    public virtual void PrimaryAction(IUsable usable)
    {
        usable.ResponseAction(this);

        if (_oneUse)
            Destroy(gameObject);
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