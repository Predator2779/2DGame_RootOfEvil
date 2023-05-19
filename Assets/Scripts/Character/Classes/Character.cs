using UnityEngine;
using GlobalVariables;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int _movementSpeed;

    private ItemHandler _itemHandler;
    private Rigidbody2D _rbody;

    public virtual void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _itemHandler = GetComponent<ItemHandler>();
    }

    #region Character

    public virtual void Use()
    {
        UseItem(GetHoldedItem(), GetUsableObject());
    }

    public void UseItem(Item item, IUsable usableObject)
    {
        if (item != null && usableObject != null && item.TryGetComponent(out UsedItem usedItem))
        {
            usedItem.Use(usableObject);
        }
    }

    public Item GetHoldedItem()
    {
        return _itemHandler.holdedItem;
    }  
    
    public IUsable GetUsableObject()
    {
        return _itemHandler.usableObject;
    }

    public void PickUpItem()
    {
       _itemHandler.PickUpItem();
    }

    public void PutItem()
    {
        _itemHandler.PutItem();
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