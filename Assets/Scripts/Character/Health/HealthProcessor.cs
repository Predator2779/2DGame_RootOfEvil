using Core.Health;
using UnityEngine;

public class HealthProcessor : MonoBehaviour, IHealth, IUsable
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] [Min(1)] private float _coefDefense;


    [SerializeField] private bool _isPassiveItem;

    public bool isPassiveItem { get { return _isPassiveItem; } }

    private Health _health;

    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        _currentHitPoints = GetCurrentHitPoints();
        _healthBar.SetCurrentHealth(_currentHitPoints);
    }

    private void Initialize()
    {
        _health = new Health(_maxHitPoints, _coefDefense);
    }

    public void ResponseAction(UsedItem item)
    {
        CheckItem(item);
    }

    public bool CheckItem(Item item)
    {
        if (item.TryGetComponent(out Weapon weapon))
        {
            TakeDamage(weapon.WeaponDamage);

            return true;
        }

        if (item.TryGetComponent(out Healer healer))
        {
            TakeHeal(healer.healPoints);

            return true;
        }

        return false;
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
        {
            _healthBar.SetCurrentHealth(_currentHitPoints);
        }
    }

    public int GetCurrentHitPoints()
    {
        return _health.GetCurrentHitPoints();
    }

    #region NotImplemented

    public void PassiveAction()
    {
        throw new System.NotImplementedException();
    }

    public void SecondaryAction(IUsable usable)
    {
        throw new System.NotImplementedException();
    }

    public void PrimaryAction()
    {
        throw new System.NotImplementedException();


        #endregion

    }
}