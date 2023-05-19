using UnityEngine;
using InputData;

public class InputHandler : MonoBehaviour
{
    [Header("Components:")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private PlayerAudioHandler audioHandler;

    private float _verticalAxis = 0;
    private float _horizontalAxis = 0;

    private Warrior _player;
    private TurnHandler _turnHandler;

    private void Awake()
    {
        _player = GetComponent<Warrior>();
        _turnHandler = GetComponent<TurnHandler>();
    }

    private void Update()
    {
        SetAxes();

        GetE();

        GetLMB_Up();

        playerAnim.SetFloat("SpeedHorizontal", Mathf.Abs(GetMovementVector().x));
        playerAnim.SetFloat("SpeedUp", GetMovementVector().y);
        playerAnim.SetFloat("SpeedDown", -GetMovementVector().y);

    }

    private void FixedUpdate()
    {
        if (IsPlayerMoving())
        {
            _player.MoveTo(GetMovementVector());

            LastPlayerSide();

            audioHandler.TakeStep();
        }
        else
        {
            _player.StopMove();

            audioHandler.StopPlaying();
        }
    }

    private void GetE()
    {
        if (InputFunctions.GetKeyE())
        {
            _player.PutItem();

            _player.PickUpItem();
        }
    }

    private void GetLMB_Up()
    {
        if (InputFunctions.GetLMB_Up())
        {
            _player.Use();
        }
    }

    private bool IsPlayerMoving()
    {
        if (_verticalAxis != 0 || _horizontalAxis != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LastPlayerSide()
    {
        if (_verticalAxis < 0)
        {
            _turnHandler.currentSide = TurnHandler.playerSides.Front;
        }
        if (_verticalAxis > 0)
        {
            _turnHandler.currentSide = TurnHandler.playerSides.Back;
        }
        if (_horizontalAxis < 0)
        {
            _turnHandler.currentSide = TurnHandler.playerSides.Left;
        }
        if (_horizontalAxis > 0)
        {
            _turnHandler.currentSide = TurnHandler.playerSides.Right;
        }
    }

    private void SetAxes()
    {
        _verticalAxis = InputFunctions.GetVerticalAxis();
        _horizontalAxis = InputFunctions.GetHorizontallAxis();
    }

    private Vector2 GetMovementVector()
    {
        var v = _player.transform.up * _verticalAxis;
        var h = _player.transform.right * _horizontalAxis;

        Vector2 vector = h + v;

        return vector;
    }
}