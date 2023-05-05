using System;
using System.Collections;
using UnityEngine;

namespace Enemies.Base
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected int attackDamage;
        [SerializeField] protected float attackRadius;
        [SerializeField] protected Transform? attackPosition;
        [SerializeField][Range(0,10)] protected int levelOfEvil;
        [SerializeField] private Character character;

        protected virtual void Update()
        {
            Flip();
        }

        private void Flip()
        {
            if(Vector2.Distance(character.transform.position, transform.position) < 40f)
                transform.eulerAngles = character!.transform.position.x > transform.position.x
                    ? new Vector3(0, 0, 0)
                    : new Vector3(0, 180, 0);
        }

        protected abstract void Hit(Collider2D col);
        
        protected abstract IEnumerator StopMove(float time);
    
    }
}
