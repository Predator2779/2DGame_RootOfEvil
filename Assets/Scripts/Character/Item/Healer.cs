using System.Collections.Generic;
using UnityEngine;

public class Healer : Weapon
{
    [SerializeField] private int _healPoints;
    [SerializeField] private List<HealthProcessor> _healthProcessors;

    public override void PrimaryAction()
    {
        foreach (HealthProcessor healthProcessor in _healthProcessors)
            healthProcessor.TakeHeal(_healPoints);

        if (_oneUse)
            Destroy(gameObject);
    }
}