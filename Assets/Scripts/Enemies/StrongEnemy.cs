using System;
using System.Collections;
using Core.Death;
using Enemies.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class StrongEnemy : BaseEnemy
    {
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackDelay;
        [SerializeField] private GameObject enemy;

        private float _currentTimer;
        private bool _canAttack = true;

        private void Awake()
        {
        }

        protected override void Update()
        {
            base.Update();

            Attack();
        }

        protected override void Hit(Collider2D col)
        {

        }

        private void Attack()
        {
            //if (attackPosition == null) return;

            if (!_canAttack) return;

            var damageHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius);

            foreach (var item in damageHit)
            {
                if (item.TryGetComponent(out DeathPlayer deathPlayer))
                {
                    deathPlayer.Damage(attackDamage);
                }
            }

            StartCoroutine(CanAttack());
        }

        private IEnumerator CanAttack()
        {
            _canAttack = false;
            yield return new WaitForSeconds(attackDelay);   
            _canAttack = true;
        }

        protected override IEnumerator StopMove(float time)
        {
            throw new System.NotImplementedException();
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (attackPosition != null) Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
        }
#endif
    }
}