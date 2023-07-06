using Core.Health;
using System.Collections;
using UnityEngine;
using GlobalVariables;

public class HealthProcessor : MonoBehaviour, IHealth
{
    [Header("Required Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private HealthBar _healthBar;

    [Header("Parameters")]
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] [Min(1)] private float _coefDefense;

    #region Private Vars

    private Health _health;

    #endregion

    private void OnEnable()
    {
        CheckPlayer();
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

        StartCoroutine(BackColor(GlobalConstants.DamageColor));

        CheckDeath();
    }

    public void TakeHeal(float heal)
    {
        _health.TakeHeal(heal);

        ChangeHealthBar();

        StartCoroutine(BackColor(GlobalConstants.HealColor));

        CheckDeath();
    }

    public void ChangeHealthBar()
    {
        if (_healthBar != null)
            _healthBar.SetCurrentHealth(_currentHitPoints);
    }

    public int GetCurrentHitPoints() { return _health.GetCurrentHitPoints(); }

    private void SetColor(Color color) => _spriteRenderer.color = color;

    private IEnumerator BackColor(Color color)//
    {
        var oldColor = _spriteRenderer.color;

        SetColor(color);

        yield return new WaitForSeconds(GlobalConstants.TimeChangeColor);

        SetColor(oldColor);
    }

    private bool CheckPlayer() { return transform.tag == "Player" ? true : false; }

    private void CheckDeath()
    {
        if (GetCurrentHitPoints() <= 0)
            if (CheckPlayer())
                SceneTransition.LoadScene(GlobalConstants.AfterDeathScene);
            else
                gameObject.SetActive(false);
    }
}