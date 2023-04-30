using UnityEngine;

public class HealthProcessor : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private float _coefDefense;
    [SerializeField] private int _hitPoints;

    private Health _health;

    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        _hitPoints = GetCurrentHitPoints();
    }

    private void Initialize()
    {
        _health = new Health(_maxHitPoints, _coefDefense);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public void TakeHeal(float heal)
    {
        _health.TakeHeal(heal);
    }

    public int GetCurrentHitPoints()
    {
        return _health.GetCurrentHitPoints();
    }
}