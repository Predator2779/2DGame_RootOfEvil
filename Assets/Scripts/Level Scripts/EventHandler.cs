using UnityEngine.Events;

public static class EventHandler
{
    public static UnityEvent OnEnemyKilled = new UnityEvent();
    public static UnityEvent OnQuestPassed = new UnityEvent();
    public static UnityEvent<int> OnEvilLevelChanged = new UnityEvent<int>();
}