using System;
using System.Collections;
using Core.Death;
using Enemies.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class WeakEnemy : BaseEnemy
    {
        [SerializeField] private float attackDelay;
        
        
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

        private void Attack()
        {
            if( attackPosition == null) return;
            if(!_canAttack) return;
            
            
            var damageHit = Physics2D.OverlapCircle(attackPosition.position, attackRadius);
            
            if(damageHit.TryGetComponent(out DeathPlayer deathPlayer))
                deathPlayer.Damage(attackDamage);

            StartCoroutine(CanAttack());
        }
        
        private IEnumerator CanAttack()
        {
            _canAttack = false;
            yield return new WaitForSeconds(attackDelay);
            _canAttack = true;
        }
        
        protected override void Hit(Collider2D col)
        {
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            Hit(col);
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