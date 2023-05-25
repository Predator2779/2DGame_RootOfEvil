using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/DialogueQuest", order = 0)]
public class DialogueQuest : ScriptableObject
{
    [Header("Quest")]
    public string questName;
    public string description;
    public bool isActive;
    public Quest prevQuest;
    public Quest additionalQuest;
    public int evilLevelQuestAvailability;

    [Header("Replicas")]
    public bool _randomSequence = false;
    public string[] replicas;
    public int countQuestAction;

    public virtual void CompleteAction()
    {
        countQuestAction++;

        if (countQuestAction >= replicas.Length)
        {
            countQuestAction = 0;

            isActive = false;
        }
    }
}