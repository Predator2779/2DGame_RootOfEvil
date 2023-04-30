interface IHealth
{
    void TakeDamage(float damage);

    void TakeHeal(float heal);

    int GetCurrentHitPoints();
}