using GlobalVariables;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [Header("Components:")]
    [SerializeField] private ItemHandler _itemHandler;
    [SerializeField] private Transform _playerSprite;
    [SerializeField] private Transform _playerTrigger;

    public enum playerSides { Front, Back, Left, Right }

    [Header("Player Side:")]
    public playerSides currentSide;

    private Character _player;

    private void Start()
    {
        _player = GetComponent<Character>();
        _itemHandler = GetComponent<ItemHandler>();

        SetPlayerSide(playerSides.Front);
    }

    public void SetPlayerSide(playerSides side)
    {
        currentSide = side;

        switch (currentSide)
        {
            case playerSides.Back:
                RotateObj(_playerSprite, 0);
                ItemLeftPos();
                _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosBack;
                break;
            case playerSides.Left:
                RotateObj(_playerSprite, 0);
                ItemLeftPos();
                _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosLeft;
                break;
            case playerSides.Right:
                RotateObj(_playerSprite, 180f);
                ItemRightPos();
                _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosRight;
                break;
            default:
                RotateObj(_playerSprite, 0);
                ItemRightPos();
                _playerTrigger.localPosition = GlobalConstants.PlayerTriggerPosFront;
                break;
        }
    }

    private bool CheckExistingItem(out Transform item)
    {
        if (_itemHandler.HoldedItem != null)
        {
            item = _itemHandler.HoldedItem.transform;

            return true;
        }
        else
        {
            item = null;

            return false;
        }
    }

    private void ItemLeftPos()
    {
        if (CheckExistingItem(out Transform item))
        {
            RotateObj(item.transform, 0);
            item.transform.localPosition = GlobalConstants.ItemPositionLeft;
        }
    }

    private void ItemRightPos()
    {
        if (CheckExistingItem(out Transform item))
        {
            RotateObj(item.transform, 180f);
            item.transform.localPosition = GlobalConstants.ItemPositionRight;
        }
    }

    private void RotateObj(Transform obj, float angle)
    {
        _player.RotateByAngle(obj, angle);
    }
}