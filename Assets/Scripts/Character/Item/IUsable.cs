public interface IUsable
{
    public void SecondaryAction();
    public void PrimaryAction(IUsable usable);
    public void ResponseAction(UsedItem item);
    public void PassiveAction();
}