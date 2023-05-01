using UnityEngine;

public class Questor : MonoBehaviour
{
    [SerializeField] private bool _isAccept = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            _isAccept = true;
        }
    }
}