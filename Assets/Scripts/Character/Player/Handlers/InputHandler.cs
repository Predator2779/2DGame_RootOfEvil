using UnityEngine;
using InputData;

public class InputHandler : MonoBehaviour
{
    #region Vars

    [Header("Components:")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private PlayerAudioHandler audioHandler;

    private Warrior _player;
    private TurnHandler _turnHandler;
    private ItemHandler _itemHandler;
    private GameModes _gameMode;

    private float _verticalAxis = 0;
    private float _horizontalAxis = 0;

    #endregion

    #region Base Methods

    private void Awake()
    {
        _player = GetComponent<Warrior>();
        _turnHandler = GetComponent<TurnHandler>();
        _itemHandler = GetComponent<ItemHandler>();

        EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);
    }

    private void Update()
    {
        if (_gameMode == GameModes.Playing)
        {
            SetAxes();

            ItemPickOrPut();

            UseItem_Primary();
            UseItem_Secondary();

            playerAnim.SetFloat("SpeedHorizontal", Mathf.Abs(GetMovementVector().x));
            playerAnim.SetFloat("SpeedUp", GetMovementVector().y);
            playerAnim.SetFloat("SpeedDown", -GetMovementVector().y);
        }
    }

    private void FixedUpdate()
    {
        if (_gameMode == GameModes.Playing)
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
    }

    #endregion

    #region Other

    private void ChangeGameMode(GameModes mode) => _gameMode = mode;

    private bool IsPlayerMoving()
    {
        if (_verticalAxis != 0 || _horizontalAxis != 0)
            return true;
        else
            return false;
    }

    #endregion

    #region Items

    private void PickUpItem()
    {
        _itemHandler.PickUpItem();

        SetPlayerSide(GetLastPlayerSide());
    }

    private void PutItem() => _itemHandler.PutItem();

    #endregion

    #region Rotation

    private TurnHandler.playerSides GetLastPlayerSide() => _turnHandler.currentSide;

    private void PlayerSideChanger()
    {
        if (_verticalAxis < 0)
            SetPlayerSide(TurnHandler.playerSides.Front);

        if (_verticalAxis > 0)
            SetPlayerSide(TurnHandler.playerSides.Back);

        if (_horizontalAxis < 0)
            SetPlayerSide(TurnHandler.playerSides.Left);

        if (_horizontalAxis > 0)
            SetPlayerSide(TurnHandler.playerSides.Right);
    }

    private void SetPlayerSide(TurnHandler.playerSides side) => _turnHandler.SetPlayerSide(side);

    #endregion

    #region Inputs

    private void SetAxes()
    {
        _verticalAxis = InputFunctions.GetVerticalAxis();
        _horizontalAxis = InputFunctions.GetHorizontalAxis();
    }

    private void ItemPickOrPut()
    {
        if (InputFunctions.GetKeyE_Up())
        {
            PutItem();

            PickUpItem();
        }
    }

    private void UseItem_Primary()
    {
        if (InputFunctions.GetLMB_Up())
            _player.UsePrimaryAction();
    } 
    
    private void UseItem_Secondary()
    {
        if (InputFunctions.GetRMB_Up())
            _player.UseSecondaryAction();
    }

    private Vector2 GetMovementVector()
    {
        var v = _player.transform.up * _verticalAxis;
        var h = _player.transform.right * _horizontalAxis;

        Vector2 vector = h + v;

        return vector;
    }

    #endregion
}