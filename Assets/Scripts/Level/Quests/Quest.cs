using System;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    [Header("Quest")]
    public string questName;
    public string description;

    [Header("Quest Options")]
    [NonSerialized] public Questor questor;
    public Quest attachedQuest;

    [Header("Base Replicas")]
    [TextArea(2, 4)]
    public string textGivingQuest;
    [TextArea(2, 4)]
    public string textNoDoneQuest;
    [TextArea(2, 4)]
    public string textCompleteQuest;

    [Header("Quest Launch Conditions")]
    public int availabilityLevel;

    //эти квесты должны быть выполнены, для того чтобы квест стал доступным
    public Quest[] requiredCompletedQuests;

    [Header("Quest States")]
    public QuestStages stage;
    public enum QuestStages
    { NotStarted, Progressing, Completed, Passed };

    public virtual bool QuestAvailability(int evilLevel)
    {
        foreach (var quest in requiredCompletedQuests)
        {
            if (quest.stage != QuestStages.Passed)
            {
                return false;
            }
        }

        if (stage != QuestStages.Passed && evilLevel <= availabilityLevel) { return true; }
        else { return false; }
    }

    public virtual void Initialize(Questor questor)
    {
        this.questor = questor;
    }

    public virtual void StartQuest()
    {
        EventHandler.OnQuestStart?.Invoke(this);
        EventHandler.OnReplicaSay?.Invoke(textGivingQuest);

        stage = QuestStages.Progressing;
    }

    public abstract void ProgressingQuest();
    public abstract void CompleteAction();
    public abstract void CheckConditions();

    public virtual bool AttachedQuestIsAvailable()
    {
        if (attachedQuest != null && attachedQuest.stage != QuestStages.Passed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void CompleteQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(textCompleteQuest);

        PassedQuest();
    }

    public virtual void PassedQuest()
    {
        questor.ChangeSprite();
        stage = QuestStages.Passed;
        EventHandler.OnQuestPassed?.Invoke();
    }
}