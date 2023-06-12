using UnityEngine.Events;

public static class EventHandler
{
    public static UnityEvent<GameModes> OnGameModeChanged = new UnityEvent<GameModes>();
    public static UnityEvent<Quest[], Questor> OnShowQuests = new UnityEvent<Quest[], Questor>();
    public static UnityEvent<bool> OnDialogueWindowShow = new UnityEvent<bool>();
    public static UnityEvent OnEnemyKilled = new UnityEvent();
    public static UnityEvent<Quest> OnQuestStart = new UnityEvent<Quest>();
    public static UnityEvent<Quest> OnOptionalQuest = new UnityEvent<Quest>();
    public static UnityEvent OnQuestPassed = new UnityEvent();
    public static UnityEvent<int> OnEvilLevelChanged = new UnityEvent<int>();
}