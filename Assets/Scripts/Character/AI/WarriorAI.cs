using UnityEngine;

[RequireComponent(typeof(Warrior))]
public class WarriorAI : CharacterAI
{
    [SerializeField] private Transform _enemy;

    protected Warrior _warrior;

    protected override void Awake() => _warrior = GetComponent<Warrior>();

    protected override void Update()
    {
        base.Update();

        CheckEnemy();
    }

    private void CheckEnemy()
    {
        if (_enemy != null && _enemy.TryGetComponent(out HealthProcessor healthProcessor))
        {
            _warrior.UsePrimaryAction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _enemy = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _enemy = null;
    }
}