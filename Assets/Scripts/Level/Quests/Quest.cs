using System;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    [Header("Quest")]
    [NonSerialized] public Questor questor;
    public string questName;
    public string description;

    [Header("Base Replicas")]
    [TextArea(2, 4)]
    public string givingQuestReplica;
    [TextArea(2, 4)]
    public string[] noDoneReplicas;
    [TextArea(2, 4)]
    public string completeQuestReplica;

    [Header("Chain Quests")]
    //на случай, если квест имеет чисто диалоговый характер и необходимо, чтобы он не зачитывался
    public bool isNotQuest = false;
    [NonSerialized] public Quest prevQuest;
    public Quest nextQuest;

    [Header("Quest Launch Conditions")]
    public int availabilityLevel = 10;

    //эти квесты должны быть выполнены, для того чтобы квест стал доступным
    public Quest[] requiredCompletedQuests;
    //квесты, завершающие данный квест.
    public Quest[] completingQuests;

    [Header("Quest States")]
    public QuestStages stage;
    public enum QuestStages
    { NotAvailable, NotStarted, Progressing, Completed, Passed };

    public virtual bool QuestAvailability(int evilLevel)
    {
        foreach (var quest in requiredCompletedQuests)
            if (quest.stage != QuestStages.Passed)
                return false;

        if (stage != QuestStages.NotAvailable &&
            stage != QuestStages.Passed &&
            evilLevel <= availabilityLevel)
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
        EventHandler.OnQuestPassed.AddListener(CompletingQuest);

        if (nextQuest != null && nextQuest.stage == QuestStages.NotAvailable)
            nextQuest.stage = QuestStages.NotStarted;

        stage = QuestStages.Progressing;
    }

    public abstract void ProgressingQuest();
    public abstract void CompleteAction();
    public abstract bool ConditionsIsDone();

    public virtual bool AttachedQuestIsAvailable()
    {
        if (nextQuest != null)
        {
            if (nextQuest.stage != QuestStages.Passed)
                return true;

            return false;
        }
        else
            return false;
    }

    public virtual void CompleteQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(completeQuestReplica);

        PassQuest();
    }

    public virtual void CompletingQuest(Quest quest)
    {
        foreach (var completingQuest in completingQuests)
            if (quest.name == completingQuest.name)
            {
                PassQuest();

                break;
            }
    }

    public virtual void PassQuest()
    {
        stage = QuestStages.Passed;
        questor.ChangeSprite();
        prevQuest.ConditionsIsDone();

        if (!isNotQuest)
            EventHandler.OnQuestPassed?.Invoke(this);
    }

    public virtual int GetRandomIndex(string[] arr)
    {
        return UnityEngine.Random.Range(0, arr.Length);
    }
}