using InputData;
using GlobalVariables;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] private Transform _skin;
    
    public Vector2 itemPlace;

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

            itemPlace = GlobalConstants.ItemPositionLeft;
        }
        else if (HorizontalAxis > 0)
        {
            _player.RotateByAngle(_skin, 180f);

            itemPlace = GlobalConstants.ItemPositionRight;
        }
    }
}