using UnityEngine;
using InputData;

public class InputHandler : MonoBehaviour
{
    private Character _player;

    private void Awake()
    {
        _player = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        _player.MoveTo(GetMovementVector());
    }

    private Vector2 GetMovementVector()
    {
        var VerticalAxis = InputFunctions.GetVerticalAxis();
        var HorizontalAxis = InputFunctions.GetHorizontallAxis();

        var v = _player.transform.up * VerticalAxis;
        var h = _player.transform.right * HorizontalAxis;

        Vector2 vector = h + v;

        return vector;
    }
}