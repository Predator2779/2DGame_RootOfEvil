using Core.Health;
using UnityEngine;

public class HealthProcessor : MonoBehaviour, IHealth
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] [Min(1)] private float _coefDefense;

    private Health _health;

    private void OnEnable()
    {
        _health = new Health(_maxHitPoints, _coefDefense);
    }

    private void Update()
    {
        _currentHitPoints = GetCurrentHitPoints();
        _healthBar.SetCurrentHealth(_currentHitPoints * 100 / _maxHitPoints);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);

        ChangeHealthBar();
    }

    public void TakeHeal(float heal)
    {
        _health.TakeHeal(heal);

        ChangeHealthBar();
    }

    public void ChangeHealthBar()
    {
        if (_healthBar != null)
            _healthBar.SetCurrentHealth(_currentHitPoints);
    }

    public int GetCurrentHitPoints()
    {
        return _health.GetCurrentHitPoints();
    }
}