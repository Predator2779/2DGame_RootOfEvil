using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    private InputHandler _inpHandler;

    private void Awake()
    {
        _inpHandler = transform.parent.GetComponent<InputHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IUsable>(out IUsable usable))
        {
            _inpHandler.usable = usable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IUsable>(out IUsable usable))
        {
            _inpHandler.usable = null;
        }
    }
}