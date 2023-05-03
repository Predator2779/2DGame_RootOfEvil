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
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackDelay;
        [SerializeField] private GameObject enemy;

        private float _currentTimer;

        private void Awake()
        {
        }

        private void Update()
        {
        }

        public override void Flip()
        {
        }

        public override void Hit(Collider2D col)
        {
            if (attackPosition == null) return;

            var damageHit = Physics2D.OverlapCircle(attackPosition.position, attackRadius);

            if (_currentTimer >= 0f)
                _currentTimer -= Time.deltaTime;
            else if (damageHit)
            {
                _currentTimer = attackDelay;
                if (damageHit.TryGetComponent(out DeathPlayer deathPlayer))
                {
                    deathPlayer.Damage(20);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            Hit(col);
        }

        public override IEnumerator StopMove(float time)
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