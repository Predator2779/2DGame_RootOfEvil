using UnityEngine;

public class Healer : Weapon
{
    [SerializeField] private int _healPoints;

    public override void PrimaryAction()
    {
        if (
            transform.parent != null &&
            transform.parent.transform.TryGetComponent(out HealthProcessor healthProcessor)
            )
            healthProcessor.TakeHeal(_healPoints);

        foreach (HealthProcessor hProcessor in _healthProcessors)
            hProcessor.TakeHeal(_healPoints);

        if (_oneUse)
            Destroy(gameObject);
    }

    public override void SecondaryAction()
    {
        if (
            transform.parent != null &&
            transform.parent.transform.TryGetComponent(out HealthProcessor healthProcessor)
            )
            healthProcessor.TakeHeal(_healPoints);

        if (_oneUse)
            Destroy(gameObject);
    }
}