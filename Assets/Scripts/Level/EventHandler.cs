using UnityEngine.Events;

public static class EventHandler
{
    public static UnityEvent<GameModes> OnGameModeChanged = new UnityEvent<GameModes>();
    public static UnityEvent OnEnemyKilled = new UnityEvent();
    public static UnityEvent<int> OnEvilLevelChanged = new UnityEvent<int>();

    #region Quest and Dialog Events

    public static UnityEvent<Quest[], Questor, int> OnShowQuests = new UnityEvent<Quest[], Questor, int>();
    public static UnityEvent<string> OnReplicaSay = new UnityEvent<string>();
    public static UnityEvent<bool> OnDialogueWindowShow = new UnityEvent<bool>();
    public static UnityEvent<Quest> OnQuestStart = new UnityEvent<Quest>();
    // Для EvilLevelCounter.
    public static UnityEvent<Quest> OnQuestPassed = new UnityEvent<Quest>();
    // Для завершения заданий.
    public static UnityEvent<Quest> OnDialogPassed = new UnityEvent<Quest>();

    #endregion
}