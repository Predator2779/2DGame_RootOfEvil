using InputData;
using GlobalVariables;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] private Transform _skin;
    [SerializeField] private ItemHandler _itemHandler;

    public enum playerSides { Front, Back, Left, Right }
    public playerSides currentSide = playerSides.Front;

    private Character _player;
    private Item _item;

    private void Start()
    {
        _player = GetComponent<Character>();
        _itemHandler = GetComponent<ItemHandler>();
    }

    private void Update()
    {
        RotatePlayer();

        if (CheckExistingItem(out Item _item))
        {
            VerticalTurnObj(_item.transform);
            HorizontalTurnObj(_item.transform);
        }
    }

    private bool CheckExistingItem(out Item item)
    {
        if (_itemHandler.holdedItem != null)
        {
            item = _itemHandler.holdedItem;

            return true;
        }
        else
        {
            item = null;

            return false;
        }
    }

    private void VerticalTurnObj(Transform obj)
    {
        if (currentSide == playerSides.Front)
        {
            obj.localPosition = GlobalConstants.ItemPositionCenter;
        }

        if (currentSide == playerSides.Back)
        {
            obj.localPosition = GlobalConstants.ItemPositionCenter;
        }
    }

    private void HorizontalTurnObj(Transform obj)
    {
        if (currentSide == playerSides.Left)
        {
            RotateObj(obj, 180f);//

            obj.localPosition = GlobalConstants.ItemPositionLeft;
        }

        if (currentSide == playerSides.Right)
        {
            RotateObj(obj, 0);//

            obj.localPosition = GlobalConstants.ItemPositionRight;
        }
    }

    private void RotatePlayer()//
    {
        if (currentSide == playerSides.Left)
        {
            RotateObj(_skin, 0);
        }

        if (currentSide == playerSides.Right)
        {
            RotateObj(_skin, 180f);
        }
    }

    private void RotateObj(Transform obj, float angle)
    {
        _player.RotateByAngle(obj, angle);
    }
}