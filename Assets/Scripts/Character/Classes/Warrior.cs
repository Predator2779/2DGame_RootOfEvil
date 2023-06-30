using System.Collections;
using UnityEngine;

public class Warrior : Character
{
    [SerializeField] private float attackRadius;
    [SerializeField][Min(1)] private int attackDamage;
    [SerializeField] private float attackDelay;

    private bool _canAttack = true;

    public override void UsePrimaryAction()
    {
        if (!_canAttack) return;

        if (holdedItem.TryGetComponent(out Weapon weapon))
            MultiplyDamage(weapon);

        StartCoroutine(CanAttack());

        base.UsePrimaryAction();
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