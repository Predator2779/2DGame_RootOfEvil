using System.Collections;
using Enemies.Base;
using UnityEngine;

namespace Enemies
{
    public class Boss : BaseEnemy
    {
        private void Start()
        {
        
        }

        protected override void Hit(Collider2D col)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator StopMove(float time)
        {
            throw new System.NotImplementedException();
        }
    }
}
