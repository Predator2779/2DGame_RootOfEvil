using UnityEngine;

public class Healer : UsedItem
{
    [SerializeField] private int _healPoints;

    [SerializeField] private IUsable _usable;

    public int HealPoints { get { return _healPoints; } }

    public override void SecondaryAction()
    {
        if (_usable != null)
            _usable.ResponseAction(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IUsable usable))
            _usable = usable;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IUsable usable) && usable == _usable)
            _usable = null;
    }
}