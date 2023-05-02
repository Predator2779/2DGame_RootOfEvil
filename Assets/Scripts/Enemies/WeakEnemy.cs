using System.Collections;
using Enemies.Base;
using UnityEngine;

namespace Enemies
{
    public class WeakEnemy : BaseEnemy
    {
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
