using UnityEngine;
using GlobalVariables;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int _movementSpeed;

    private Rigidbody2D _rbody;
    private Animator _animator;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attack();
        }
    }

    private void Attack()
    {
        
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
}