using Core.Death;
using System.Collections;
using UnityEngine;

public class Warrior : Character
{
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackDelay;

    private bool _canAttack = true;

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_canAttack) return;

            var damageHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius);
            foreach (var hit in damageHit)
            {
                if (hit.TryGetComponent(out DeathEnemy deathEnemy))
                    deathEnemy.Damage(attackDamage);
            }

            StartCoroutine(CanAttack());
        }
    }

    private IEnumerator CanAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (attackPosition != null) Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }
#endif
}