using UnityEngine;

public class Weapon : UsedItem, IUsable
{
    [SerializeField] private bool _isPassiveItem;
    [SerializeField] private int _weaponDamage;
    [Min(1)] private int _damageFactor;

    public bool isPassiveItem { get { return _isPassiveItem; } }
    public int WeaponDamage { get { return _weaponDamage * _damageFactor; } }
    public int DamageFactor { set { _damageFactor = value; } }

    #region NotImplemented

    public void SecondaryAction(IUsable usable)
    {
        throw new System.NotImplementedException();
    }

    public void PassiveAction()
    {
        throw new System.NotImplementedException();
    }

    public void PrimaryAction()
    {
        throw new System.NotImplementedException();
    }

    public void ResponseAction(UsedItem item)
    {
        throw new System.NotImplementedException();
    }

    public bool CheckItem(Item item)
    {
        throw new System.NotImplementedException();
    }

    #endregion
}