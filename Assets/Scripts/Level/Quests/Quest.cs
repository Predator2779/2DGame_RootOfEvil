using System;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    [Header("Quest")]
    public string questName;
    public string description;

    [Header("Quest Options")]
    [NonSerialized] public Questor questor;
    public Quest[] attachedQuests;

    [Header("Base Replicas")]
    [TextArea(2, 4)]
    public string givingQuestReplica;
    [TextArea(2, 4)]
    public string[] noDoneReplicas;
    [TextArea(2, 4)]
    public string completeQuestReplica;

    [Header("Quest Launch Conditions")]
    public int availabilityLevel = 10;

    //эти квесты должны быть выполнены, для того чтобы квест стал доступным
    public Quest[] requiredCompletedQuests;

    [Header("Quest States")]
    public QuestStages stage;
    public enum QuestStages
    { NotAvailable, NotStarted, Progressing, Completed, Passed };

    public virtual bool QuestAvailability(int evilLevel)
    {
        foreach (var quest in requiredCompletedQuests)
            if (quest.stage != QuestStages.Passed)
                return false;

        if (stage != QuestStages.NotAvailable && stage != QuestStages.Passed && evilLevel <= availabilityLevel)
            return true;
        else
            return false;
    }

    public virtual void Initialize(Questor questor)
    {
        this.questor = questor;
    }

    public virtual void StartQuest()
    {
        EventHandler.OnQuestStart?.Invoke(this);
        EventHandler.OnReplicaSay?.Invoke(givingQuestReplica);

        foreach (var quest in attachedQuests)
            if (quest.stage == QuestStages.NotAvailable)
                quest.stage = QuestStages.NotStarted;

        stage = QuestStages.Progressing;
    }

    public abstract void ProgressingQuest();
    public abstract void CompleteAction();
    public abstract bool ConditionsIsDone();

    public virtual bool AttachedQuestIsAvailable()
    {
        if (attachedQuests != null)
        {
            foreach (var quest in attachedQuests)
                if (quest.stage != QuestStages.Passed)
                    return true;

            return false;
        }
        else
            return false;
    }

    public virtual void CompleteQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(completeQuestReplica);

        PassedQuest();
    }

    public virtual void PassedQuest()
    {
        questor.ChangeSprite();
        stage = QuestStages.Passed;
        EventHandler.OnQuestPassed?.Invoke();
    }

    public virtual int GetRandomIndex(string[] arr)
    {
        return UnityEngine.Random.Range(0, arr.Length);
    }
}