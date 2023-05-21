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
    private ItemHandler _itemHandler;

    private void Awake()
    {
        _player = GetComponent<Warrior>();
        _turnHandler = GetComponent<TurnHandler>();
        _itemHandler = GetComponent<ItemHandler>();

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

            PlayerSideChanger();

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
            PutItem();

            PickUpItem();
        }
    }

    private void PutItem()
    {
        _itemHandler.PutItem();
    } 
    
    private void PickUpItem()
    {
        _itemHandler.PickUpItem();

        SetPlayerSide(GetLastPlayerSide());
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

    private TurnHandler.playerSides GetLastPlayerSide()
    {
       return _turnHandler.currentSide;
    }

    private void PlayerSideChanger()
    {
        if (_verticalAxis < 0)
        {
            SetPlayerSide(TurnHandler.playerSides.Front);
        }
        if (_verticalAxis > 0)
        {
            SetPlayerSide(TurnHandler.playerSides.Back);
        }
        if (_horizontalAxis < 0)
        {
            SetPlayerSide(TurnHandler.playerSides.Left);
        }
        if (_horizontalAxis > 0)
        {
            SetPlayerSide(TurnHandler.playerSides.Right);
        }
    }

    private void SetPlayerSide(TurnHandler.playerSides side)
    {
        _turnHandler.SetPlayerSide(side);
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