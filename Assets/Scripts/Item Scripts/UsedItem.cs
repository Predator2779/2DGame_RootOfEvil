public class UsedItem : Item
{
    public virtual void Use(IUsable usable)
    {
        usable.ApplyItem(this);
    }
}