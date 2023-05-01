using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform? attackPosition;
    
    public  GameObject?  player { get; protected internal set; }

    //public abstract void Flip();
    
    protected virtual void Hit(Collider2D col)
    { }
    
    
    
    void Update()
    {
        
    }
}
