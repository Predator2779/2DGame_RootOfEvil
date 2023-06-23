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


    public void ChangeStage(QuestStages stage) => this.stage = stage;

    public void CheckQuest()
    {
        switch (stage)
        {
            case QuestStages.NotStarted:
                StartQuest();
                break;
            case QuestStages.Progressing:
                ProgressingQuest();
                break;
            case QuestStages.Completed:
                PassQuest();
                break;
        }
    }

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

    public virtual void Initialize(Questor questor) => this.questor = questor;

    public virtual void StartQuest()
    {
        MakeAvailableQuests(availableAfterStart);

        EventHandler.OnQuestStart?.Invoke(this);
        EventHandler.OnReplicaSay?.Invoke(givingReplica);
        EventHandler.OnQuestStart.AddListener(PassingQuestAfterStart);
        EventHandler.OnQuestComplete.AddListener(PassingQuestAfterComplete);
        EventHandler.OnDialogPassed.AddListener(PassingQuestAfterPass);

        if (nextQuest?.stage == QuestStages.NotAvailable)
        {
            nextQuest.ChangeStage(QuestStages.NotStarted);

            nextQuest.prevQuest = this;
        }

        ChangeStage(QuestStages.Progressing);
    }

    public virtual bool ConditionsIsDone()
    {
        if (SomeCondition() && !AttachedQuestIsAvailable())
        {
            CompleteQuest();

            return true;
        }
        else
            return false;
    }

    public virtual void ProgressingQuest()
    {
        if (ConditionsIsDone())
            PassQuest();
        else
            NoDone();
    }

    public void CompleteQuest()
    {
        EventHandler.OnQuestComplete?.Invoke(this);

        MakeAvailableQuests(availableAfterComplete);

        ChangeStage(QuestStages.Completed);
    }

    public virtual void PassQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(completeReplica);

        questor.ChangeSprite();
        prevQuest?.ConditionsIsDone();

        if (type == QuestType.Quest)
            EventHandler.OnQuestPassed?.Invoke(this);

        MakeAvailableQuests(availableAfterPass);

        ChangeStage(QuestStages.Passed);
    }

    public abstract bool SomeCondition();
    public abstract void CompleteAction();
    public abstract void NoDone();

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

    #region Other Methods

    public virtual int GetRandomIndex(string[] arr)
    {
        return UnityEngine.Random.Range(0, arr.Length);
    }

    public virtual bool AttachedQuestIsAvailable()
    {
        if (nextQuest != null && nextQuest?.stage != QuestStages.Passed)
            return true;
        else
            return false;
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

    #endregion
}