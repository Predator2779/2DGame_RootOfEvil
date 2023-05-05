using System.Collections;
using Enemies.Base;
using UnityEngine;

namespace Enemies
{
    public class StrongEnemy : BaseEnemy
    {

        protected override void Hit(Collider2D col)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator StopMove(float time)
        {
            throw new System.NotImplementedException();
        }
    #if UNITY_EDITOR
    #endif
    }
}
