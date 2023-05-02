using System.Collections;
using Enemies.Base;
using UnityEngine;

namespace Enemies
{
    public abstract class WeakEnemy : BaseEnemy
    {
        private void Start()
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
            
        }

        public override IEnumerator StopMove(float time)
        {
            throw new System.NotImplementedException();
        }
    }
}
