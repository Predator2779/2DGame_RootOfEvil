using UnityEngine;
using InputData;

public class InputHandler : MonoBehaviour
{
    public Animator animator;
    public PlayerAudioHandler audioHandler;
    public IUsable usable;

    private Character _player;
    private ItemHandler _itemHandler;

    private void Awake()
    {
        _player = GetComponent<Character>();
        _itemHandler = GetComponent<ItemHandler>();
    }

    private void Update()
    {
        GetE();

        GetLMB();
    }

    private void FixedUpdate()
    {
        _player.MoveTo(GetMovementVector());
                
        animator.SetFloat("SpeedHorizontal", Mathf.Abs(GetMovementVector().x));
        animator.SetFloat("SpeedUp", GetMovementVector().y);
        animator.SetFloat("SpeedDown", -GetMovementVector().y);

        if (GetMovementVector() != new Vector2(0, 0))
        {
            audioHandler.TakeStep();
        }
        else
        {
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
        if (usable != null)
        {
            _itemHandler.UseAnObject(usable);
        }
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