using Core.Health;
using System.Collections;
using UnityEngine;

public class HealthProcessor : MonoBehaviour, IHealth
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] [Min(1)] private float _coefDefense;
    [SerializeField] private float _timeOfColor = 0.01f; // добавить в глобал файл.
    [SerializeField] private Color _healColor;
    [SerializeField] private Color _damageColor;

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

        StartCoroutine(BackColor(_damageColor));
    }

    public void TakeHeal(float heal)
    {
        _health.TakeHeal(heal);

        ChangeHealthBar();

        StartCoroutine(BackColor(_healColor));
    }

    public void ChangeHealthBar()
    {
        if (_healthBar != null)
            _healthBar.SetCurrentHealth(_currentHitPoints);
    }

    public int GetCurrentHitPoints() { return _health.GetCurrentHitPoints(); }

    private void SetColor(Color color) => _spriteRenderer.color = color;

    private IEnumerator BackColor(Color32 color)
    {
        var oldColor = _spriteRenderer.color;

        //Color f = Color.HSVToRGB(117, 220, 225);

        SetColor(color);

        yield return new WaitForSeconds(_timeOfColor);

        SetColor(oldColor);
    }
}