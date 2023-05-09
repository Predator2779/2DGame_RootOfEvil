using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using Core.Death;
using UnityEngine;
using GlobalVariables;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private int _movementSpeed;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackDelay;

    private bool _canAttack = true;
    private Rigidbody2D _rbody;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_canAttack) return;

            var damageHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius);
            foreach (var hit in damageHit)
            {
                if (hit.TryGetComponent(out DeathEnemy deathEnemy))
                    deathEnemy.Damage(attackDamage);
            }

            StartCoroutine(CanAttack());
        }
    }

    private IEnumerator CanAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }

    public void MoveTo(Vector2 movementDirection)
    {
        float speed = _movementSpeed * GlobalConstants.CoefMovementSpeed;

        ExecuteCommand(new MoveCommand(_rbody, movementDirection * speed));
    }

    public void RotateByAngle(Transform obj, float angle)
    {
        ExecuteCommandByValue(new RotationCommand(obj), angle);
    }

    private void ExecuteCommand(Command command)
    {
        command.Execute();
    }

    private void ExecuteCommandByValue(Command command, float value)
    {
        command.ExecuteByValue(value);
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (attackPosition != null) Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }
#endif
}