using System.Collections;
using UnityEngine;

public class Warrior : Character
{
    [SerializeField] private float attackRadius;
    [SerializeField][Min(1)] private int attackDamage;
    [SerializeField] private float attackDelay;

    private bool _canAttack = true;

    public override void Use()
    {
        Attack();
    }

    public void Attack()
    {
        if (!_canAttack) return;

        Item item = holdedItem;

        if (CheckUsing(item, usableObject) && item.TryGetComponent(out Weapon weapon))
        {
            item = MultiplyDamage(weapon);
        }

        UseItem(item, usableObject);

        StartCoroutine(CanAttack());
    }

    private Weapon MultiplyDamage(Weapon weapon)
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