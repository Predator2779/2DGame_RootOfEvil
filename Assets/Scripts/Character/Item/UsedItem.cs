using UnityEngine;

public class UsedItem : Item
{
    [Tooltip("����������� �������� �������� ����� �������������")]
    [SerializeField] private bool _oneUse = false;

    public virtual void Use(IUsable usable)
    {
        usable.ResponseAction(this);

        if (_oneUse)
            Destroy(gameObject);
    }
}