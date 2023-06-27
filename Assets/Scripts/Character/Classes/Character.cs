using UnityEngine;
using GlobalVariables;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int _movementSpeed;

    public Item holdedItem;
    public IUsable usableObject;

    private Rigidbody2D _rbody;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    #region Character

    public virtual void PickOrPut()
    {
        UseItem(holdedItem, usableObject);
    }

    public void UseItem(Item item, IUsable usableObject)
    {
        if (CheckUsing(item, usableObject) && item.TryGetComponent(out UsedItem usedItem))
        {
            usedItem.Use(usableObject);
        }
    }
    public bool CheckUsing(Item item, IUsable usableObject)
    {
        if (item != null && usableObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    private void ExecuteCommand(Command command)
    {
        command.Execute();
    }

    private void ExecuteCommandByValue(Command command, float value)
    {
        command.ExecuteByValue(value);
    }

    #endregion
}