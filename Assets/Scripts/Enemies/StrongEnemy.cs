using System.Collections;
using Enemies.Base;
using UnityEngine;

namespace Enemies
{
    public class StrongEnemy : BaseEnemy
    {

        private void Update()
        {
        
        }

        public override void Flip()
        {
        }

        public override void Hit(Collider2D col)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator StopMove(float time)
        {
            throw new System.NotImplementedException();
        }
    #if UNITY_EDITOR
    #endif
    }
}
