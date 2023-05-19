using System.Collections;
using UnityEngine;

public class Warrior : Character
{
    [SerializeField] private float attackRadius;
    [SerializeField][Min(1)] private int attackDamage;
    [SerializeField] private float attackDelay;

    private bool _canAttack = true;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
        if (!_canAttack) return;

        Item item = GetHoldedItem();

        if (item != null && item.TryGetComponent(out Weapon weapon))
        {
            item = MultiplyDamage(weapon);
        }

        UseItem(item, GetUsableObject());

        StartCoroutine(CanAttack());
    }

    public Weapon MultiplyDamage(Weapon weapon)
    {
        weapon.DamageFactor = attackDamage;

        return weapon;
    }

    private IEnumerator CanAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }
}