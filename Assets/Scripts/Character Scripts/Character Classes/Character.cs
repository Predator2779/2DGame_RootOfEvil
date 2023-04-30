using UnityEngine;
using GlobalVariables;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    protected Rigidbody2D _rbody;
    [SerializeField][Range(0, 10)] protected int _movementSpeed;

    private void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 movementDirection)
    {
        float speed = _movementSpeed * GlobalConstants.CoefMovementSpeed;

        ExecuteCommand(new MoveCommand(_rbody, movementDirection * speed));
    }

    public void RotateTo(Transform obj, Vector2 target)
    {
        ExecuteCommand(new RotationCommand(obj, target));
    }

    public void RotateByAngle(Transform obj, float angle)
    {
        ExecuteCommandByValue(new RotationCommand(obj), angle);
    }

    protected void ExecuteCommand(Command command)
    {
        command.Execute();
    }
    
    protected void ExecuteCommandByValue(Command command, float value)
    {
        command.ExecuteByValue(value);
    }
}