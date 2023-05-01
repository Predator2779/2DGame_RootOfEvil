using UnityEngine;

public class Weapon : Item
{
    public float baseDamage = 1; 

    public override void Use()
    {
        base.Use();

        TakeDamage();
    }

    private void TakeDamage()
    {

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);


    }
}