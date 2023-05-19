using UnityEngine;

public class Following : MonoBehaviour
{
    [SerializeField] private Transform _trackingObject;

    private void FixedUpdate()
    {
        transform.position = _trackingObject.position;
    }
}