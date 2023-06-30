using UnityEngine;

public class Weapon : UsedItem
{
    [SerializeField] private int _weaponDamage;
    [Min(1)] private int _damageFactor;

    public int WeaponDamage { get { return _weaponDamage * _damageFactor; } }
    public int DamageFactor { set { _damageFactor = value; } }

    //public override void PrimaryAction(IUsable usable)
    //{
    //    usable.ResponseAction(this);

    //    if (_oneUse)
    //        Destroy(gameObject);
    //}
}