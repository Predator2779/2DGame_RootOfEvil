using UnityEngine;
using GlobalVariables;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;

    private Rigidbody2D _rbody;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var force = _speedMovement * GetMovementVector();

        _rbody.AddForce(force, ForceMode2D.Force);
    }

    private Vector2 GetMovementVector()
    {
        var VerticalAxis = Input.GetAxis(GlobalConstants.VerticalAxis);
        var HorizontalAxis = Input.GetAxis(GlobalConstants.HorizontalAxis);

        var v = Vector2.up * VerticalAxis;
        var h = Vector2.right * HorizontalAxis;

        Vector2 vector = h + v;

        return vector.normalized;
    }
}