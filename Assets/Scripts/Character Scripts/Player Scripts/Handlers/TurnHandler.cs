using InputData;
using GlobalVariables;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public Vector2 ItemPlace;

    [SerializeField] private Transform _skin;
    private Character _player;

    private void Start()
    {
        _player = GetComponent<Character>();

        HorizontalTurnPlayer();
    }

    private void Update()
    {
        HorizontalTurnPlayer();
    }

    private void HorizontalTurnPlayer()
    {
        var HorizontalAxis = InputFunctions.GetHorizontallAxis();

        if (HorizontalAxis < 0)
        {
            _player.RotateByAngle(_skin, 0);

            ItemPlace = GlobalConstants.ItemPositionLeft;
        }
        else if (HorizontalAxis > 0)
        {
            _player.RotateByAngle(_skin, 180f);

            ItemPlace = GlobalConstants.ItemPositionRight;
        }
    }
}