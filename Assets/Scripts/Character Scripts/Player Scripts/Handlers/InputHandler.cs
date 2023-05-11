using UnityEngine;
using InputData;

public class InputHandler : MonoBehaviour
{
    public Animator animator;
    public PlayerAudioHandler audioHandler;

    private Character _player;
    private ItemHandler _itemHandler;
    private TurnHandler _turnHandler;

    [SerializeField] private float _verticalAxis = 0;
    [SerializeField] private float _horizontalAxis = 0;

    private void Awake()
    {
        _player = GetComponent<Character>();
        _turnHandler = GetComponent<TurnHandler>();
        _itemHandler = GetComponent<ItemHandler>();
    }

    private void Update()
    {
        GetAxis();

        GetE();

        GetLMB();
    }

    private void FixedUpdate()
    {
        animator.SetFloat("SpeedHorizontal", Mathf.Abs(GetMovementVector().x));
        animator.SetFloat("SpeedUp", GetMovementVector().y);
        animator.SetFloat("SpeedDown", -GetMovementVector().y);

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
            _itemHandler.PutItem();

            _itemHandler.PickUpItem();
        }
    }

    private void GetLMB()
    {
        if (InputFunctions.GetLMB())
        {
            _itemHandler.UseItem();
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

    private void GetAxis()
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