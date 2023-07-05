using Core.Health;
using System.Collections;
using UnityEngine;
using GlobalVariables;

public class HealthProcessor : MonoBehaviour, IHealth
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] [Min(1)] private float _coefDefense;

    private Health _health;

    private void OnEnable() => _health = new Health(_maxHitPoints, _coefDefense);

    private void Update()
    {
        _currentHitPoints = GetCurrentHitPoints();
        _healthBar.SetCurrentHealth(_currentHitPoints * 100 / _maxHitPoints);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);

        ChangeHealthBar();

        StartCoroutine(BackColor(GlobalConstants.DamageColor));
    }

    public void TakeHeal(float heal)
    {
        _health.TakeHeal(heal);

        ChangeHealthBar();

        StartCoroutine(BackColor(GlobalConstants.HealColor));
    }

    public void ChangeHealthBar()
    {
        if (_healthBar != null)
            _healthBar.SetCurrentHealth(_currentHitPoints);
    }

    public int GetCurrentHitPoints() { return _health.GetCurrentHitPoints(); }

    private void SetColor(Color color) => _spriteRenderer.color = color;

    private IEnumerator BackColor(Color color)
    {
        var oldColor = _spriteRenderer.color;

        SetColor(color);

        yield return new WaitForSeconds(GlobalConstants.TimeChangeColor);

        SetColor(oldColor);
    }
}