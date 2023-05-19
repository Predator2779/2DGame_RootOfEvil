public interface IUsable
{
    public void PassiveAction();

    public void ResponseAction(UsedItem item);

    public void PrimaryAction();

    public void SecondaryAction(IUsable usable);
}