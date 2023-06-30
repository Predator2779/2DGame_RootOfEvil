using UnityEngine;
using GlobalVariables;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int _movementSpeed;

    public Item holdedItem;

    private Rigidbody2D _rbody;

    private void Awake() => _rbody = GetComponent<Rigidbody2D>();

    #region Character

    public virtual void UsePrimaryAction()
    {
        if (
            holdedItem != null &&
            holdedItem.TryGetComponent(out UsedItem usedItem)
            )
            usedItem.PrimaryAction();//
    }

    public void UseSecondaryAction()
    {
        if (
            holdedItem != null &&
            holdedItem.TryGetComponent(out UsedItem usedItem)
            )
            usedItem.SecondaryAction();
    }

    public void MoveTo(Vector2 movementDirection)
    {
        float speed = _movementSpeed * GlobalConstants.CoefMovementSpeed;

        ExecuteCommand(new MoveCommand(_rbody, movementDirection * speed));
    }

    public void StopMove()
    {
        ExecuteCommand(new MoveCommand(_rbody, Vector2.zero * 0));
    }

    public void RotateByAngle(Transform obj, float angle)
    {
        ExecuteCommandByValue(new RotationCommand(obj), angle);
    }

    #endregion

    #region Common

    private void ExecuteCommand(Command command) => command.Execute();

    private void ExecuteCommandByValue(Command command, float value) => command.ExecuteByValue(value);

    #endregion
}