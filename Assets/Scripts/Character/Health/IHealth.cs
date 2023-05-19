public interface IHealth
{
    public void TakeDamage(float damage);

    public void TakeHeal(float heal);

    public int GetCurrentHitPoints();
}