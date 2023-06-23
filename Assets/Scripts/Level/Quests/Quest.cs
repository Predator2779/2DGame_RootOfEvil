using System;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    #region Vars

    [Header("Quest")]
    public string questName;
    public string description;
    [Tooltip("На случай, если квест имеет чисто диалоговый характер и необходимо, чтобы он не зачитывался")]
    private enum QuestType { Quest, Dialog }
    [SerializeField] private QuestType type = QuestType.Quest;
    [NonSerialized] public Questor questor;

    [Header("Base Replicas")]
    [TextArea(2, 4)]
    public string givingReplica;
    [TextArea(2, 4)]
    public string[] noDoneReplicas;
    [TextArea(2, 4)]
    public string completeReplica;

    [Header("Chain Quests")]
    [NonSerialized] public Quest prevQuest;
    public Quest nextQuest;
    [Tooltip("Квесты, завершающие данный квест после начала этого")]
    public Quest[] passingQuestsAfterStart;
    [Tooltip("Квесты, завершающие данный квест после выполнения этого")]
    public Quest[] passingQuestsAfterComplete;
    [Tooltip("Квесты, завершающие данный квест после завершения этого")]
    public Quest[] passingQuestsAfterPass;

    [Header("Requirements Quest Launch")]
    [Tooltip("Требуемый уровень зла в мире (ниже указанного)")]
    public int availabilityLevel = 10;
    [Tooltip("Эти квесты должны быть начаты, для того чтобы квест стал доступным")]
    public Quest[] requiredStartedQuests;
    [Tooltip("Эти квесты должны быть выполнены, для того чтобы квест стал доступным")]
    public Quest[] requiredCompletedQuests;
    [Tooltip("Эти квесты должны быть завершены, для того чтобы квест стал доступным")]
    public Quest[] requiredPassedQuests;

    [Header("Make Available after this Quests")]
    [Tooltip("Делает доступными эти квесты, после старта этого")]
    public Quest[] availableAfterStart;
    [Tooltip("Делает доступными эти квесты, после выполнения этого")]
    public Quest[] availableAfterComplete;
    [Tooltip("Делает доступными эти квесты, после завершения этого")]
    public Quest[] availableAfterPass;

    [Header("Quest States")]
    public QuestStages stage;
    public enum QuestStages
    { NotAvailable, NotStarted, Progressing, Completed, Passed };

    #endregion

    #region Base Methods

    public virtual bool QuestAvailability(int evilLevel)
    {
        if (
            IsMeetsTheRequirements(requiredStartedQuests, QuestStages.Progressing) &&
            IsMeetsTheRequirements(requiredCompletedQuests, QuestStages.Completed) &&
            IsMeetsTheRequirements(requiredPassedQuests, QuestStages.Passed) &&
            stage != QuestStages.NotAvailable &&
            stage != QuestStages.Passed &&
            evilLevel <= availabilityLevel
            )
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
        if (nextQuest?.stage == QuestStages.NotAvailable)
        {
            nextQuest.ChangeStage(QuestStages.NotStarted);

            nextQuest.prevQuest = this;
        }

        ChangeStage(QuestStages.Progressing);
    }

    public abstract void ProgressingQuest();
    public abstract void CompleteAction();
    public abstract bool ConditionsIsDone();

    public virtual bool AttachedQuestIsAvailable()
    {
        if (nextQuest != null && nextQuest?.stage != QuestStages.Passed)
            return true;
        else
            return false;
    }

    public virtual void CompleteQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(completeReplica);

        PassQuest();
    }

    public virtual void PassQuest()
    {
        ChangeStage(QuestStages.Passed);

        questor.ChangeSprite();
        prevQuest?.ConditionsIsDone();

        if (type == QuestType.Quest)
            EventHandler.OnQuestPassed?.Invoke(this);
    }

    #endregion

    #region Passing Quests from other Quest

    public virtual void PassingQuestAfterStart(Quest quest) => PassingQuest(quest, passingQuestsAfterStart);

    public virtual void PassingQuestAfterComplete(Quest quest) => PassingQuest(quest, passingQuestsAfterComplete);

    public virtual void PassingQuestAfterPass(Quest quest) => PassingQuest(quest, passingQuestsAfterPass);

    public virtual void PassingQuest(Quest quest, Quest[] quests)
    {
        foreach (var passingQuest in passingQuestsAfterPass)
            if (quest.name == passingQuest.name)
            {
                PassQuest();

                break;
            }
    }

    #endregion

    public void ChangeStage(QuestStages stage)
    {
        this.stage = stage;

        switch (this.stage)
        {
            case QuestStages.Progressing:
                MakeAvailableQuests(availableAfterStart);
                EventHandler.OnQuestStart?.Invoke(this);
                EventHandler.OnReplicaSay?.Invoke(givingReplica);
                EventHandler.OnQuestStart.AddListener(PassingQuestAfterStart);
                EventHandler.OnQuestComplete.AddListener(PassingQuestAfterComplete);
                EventHandler.OnDialogPassed.AddListener(PassingQuestAfterPass);
                break;
            case QuestStages.Completed:
                MakeAvailableQuests(availableAfterComplete);
                EventHandler.OnQuestComplete?.Invoke(this);
                break;
            case QuestStages.Passed:
                MakeAvailableQuests(availableAfterPass);
                EventHandler.OnDialogPassed?.Invoke(this);
                break;
        }
    }

    public virtual int GetRandomIndex(string[] arr)
    {
        return UnityEngine.Random.Range(0, arr.Length);
    }

    private void MakeAvailableQuests(Quest[] quests)
    {
        foreach (var quest in quests)
            quest.ChangeStage(QuestStages.NotStarted);
    }

    private bool IsMeetsTheRequirements(Quest[] quests, QuestStages requiredStage)
    {
        if (quests != null)
            foreach (var quest in quests)
                if (quest.stage != requiredStage)
                    return false;

        return true;
    }
}