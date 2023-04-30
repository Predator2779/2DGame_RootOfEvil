using UnityEngine;
using InputData;

public class InputHandler : MonoBehaviour
{
    private Character _player;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        _player.MoveTo(GetMovementVector());
    }

    private void Initialize()
    {
        _player = GetComponent<Character>();
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