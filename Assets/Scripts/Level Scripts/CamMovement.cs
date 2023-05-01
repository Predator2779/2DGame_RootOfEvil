using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out InputHandler inputHandler))
        {
            _camera.transform.position = transform.position;
        }
    }
}