using System.Collections.Generic;
using UnityEngine;

public class Weapon : UsedItem
{
    [SerializeField] private int _weaponDamage;
    [SerializeField] protected List<HealthProcessor> _healthProcessors;

    private int _damageFactor;
    public int DamageFactor { set { _damageFactor = value; } }

    public override void PrimaryAction()
    {
        foreach (HealthProcessor healthProcessor in _healthProcessors)
            healthProcessor.TakeDamage(_weaponDamage * _damageFactor);

        if (_oneUse)
            Destroy(gameObject);
    }

    public override void AddToList(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthProcessor healthProcessors))
            if (!_healthProcessors.Contains(healthProcessors))
                _healthProcessors.Add(healthProcessors);
    }

    public override void RemoveFromList(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthProcessor healthProcessors))
            if (_healthProcessors.Contains(healthProcessors))
                _healthProcessors.Remove(healthProcessors);
    }
}