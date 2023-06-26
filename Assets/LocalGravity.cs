using UnityEngine;

public class LocalGravity : MonoBehaviour
{
    private Rigidbody2D _rbody;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rbody.AddForce(-Vector2.up * 9.81f, ForceMode2D.Force);
    }
}