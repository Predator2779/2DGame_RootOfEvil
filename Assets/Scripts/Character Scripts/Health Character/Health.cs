using GlobalVariables;
using UnityEngine;

public class Health : IHealth
{
    private readonly int _minHitPoints = GlobalConstants.MinHitPoints;
    private readonly int _maxHitPoints;
    private readonly float _coefDefense;
    private int _hitPoints;

    #region Конструктор

    public Health(int MaxHitPoints, float CoefDefense)
    {
        _maxHitPoints = MaxHitPoints;
        _hitPoints = MaxHitPoints;
        _coefDefense = CoefDefense;
    }
    public Health(int MaxHitPoints, float CoefDefense, int StartHitPoints)
    {
        _maxHitPoints = MaxHitPoints;
        _hitPoints = StartHitPoints;
        _coefDefense = CoefDefense;
    }

    #endregion

    public void TakeDamage(float damage)
    {
        ApplyValue(-damage / _coefDefense);
    }

    public void TakeHeal(float heal)
    {
        ApplyValue(heal);
    }

    public int GetCurrentHitPoints()
    {
        return _hitPoints;
    }

    private void ApplyValue(float value)
    {
        float hitPoints = _hitPoints + value;

        if (hitPoints > _maxHitPoints)
        {
            _hitPoints = _maxHitPoints;
        }
        else if (hitPoints < _minHitPoints)
        {
            _hitPoints = _minHitPoints;
        }
        else
        {
            _hitPoints += (int)value;
        }
    }
}