using EditorExtension;
using System;
using System.IO;
using UnityEditor;
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

    [Header("Quest States")]
    public QuestStages stage;
    public enum QuestStages
    { NotAvailable, NotStarted, Progressing, Completed, Passed };

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

    [Header("Requirements Quest Launch")]
    [Tooltip("Требуемый уровень зла в мире (ниже указанного)")]
    public int availabilityLevel = 10;
    [Tooltip("Эти квесты должны быть начаты, для того чтобы квест стал доступным")]
    public Quest[] requiredQuests_start;
    [Tooltip("Эти квесты должны быть выполнены, для того чтобы квест стал доступным")]
    public Quest[] requiredQuests_complete;
    [Tooltip("Эти квесты должны быть завершены, для того чтобы квест стал доступным")]
    public Quest[] requiredQuests_pass;

    [Header("Change other Quests")]
    [Tooltip("Квесты, завершающие данный квест после их начала")]
    public Quest[] passQuests_start;
    [Tooltip("Квесты, завершающие данный квест после их выполнения")]
    public Quest[] passQuests_complete;
    [Tooltip("Квесты, завершающие данный квест после их завершения")]
    public Quest[] passQuests_pass;

    [Header("Make Available after this Quests")]
    [Tooltip("Делает доступными эти квесты, после старта этого")]
    public Quest[] availableQuests_start;
    [Tooltip("Делает доступными эти квесты, после выполнения этого")]
    public Quest[] availableQuests_complete;
    [Tooltip("Делает доступными эти квесты, после завершения этого")]
    public Quest[] availableQuests_pass;

    #endregion

    #region Base Methods

    public virtual bool QuestAvailability(int evilLevel)
    {
        if (stage != QuestStages.NotAvailable && stage != QuestStages.Passed &&
            evilLevel <= availabilityLevel && CheckRequiredQuests())
            return true;
        else
            return false;
    }

    public virtual void Initialize(Questor questor)
    {
        this.questor = questor;
    }

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

    public virtual void StartQuest()
    {
        MakeAvailableQuests(availableQuests_start);

        EventHandler.OnQuestStart?.Invoke(this);
        EventHandler.OnReplicaSay?.Invoke(questor, givingReplica);

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

    public virtual void ProgressingQuest()
    {
        if (ConditionsIsDone())
            PassQuest();
        else
            NoDone();
    }

    public void CompleteQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(questor, completeReplica);
        EventHandler.OnQuestComplete?.Invoke(this);

        MakeAvailableQuests(availableQuests_complete);

        ChangeStage(QuestStages.Completed);
    }

    public virtual void PassQuest()
    {
        questor.SetSmileSprite();
        prevQuest?.ConditionsIsDone();

        if (type == QuestType.Quest)
            EventHandler.OnQuestPassed?.Invoke(this);

        MakeAvailableQuests(availableQuests_pass);

        ChangeStage(QuestStages.Passed);
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

    public abstract bool SomeCondition();
    public abstract void CompleteAction();
    public abstract void NoDone();

    #endregion

    #region Passing Quests from other Quest

    public virtual void PassingQuestAfterStart(Quest quest) => PassingQuest(quest, passQuests_start);

    public virtual void PassingQuestAfterComplete(Quest quest) => PassingQuest(quest, passQuests_complete);

    public virtual void PassingQuestAfterPass(Quest quest) => PassingQuest(quest, passQuests_pass);

    public virtual void PassingQuest(Quest quest, Quest[] quests)
    {
        foreach (var passingQuest in quests)
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

    private bool CheckRequiredQuests()
    {
        if (
            IsMeetsTheRequirements(requiredQuests_start, QuestStages.Progressing) &&
            IsMeetsTheRequirements(requiredQuests_complete, QuestStages.Completed) &&
            IsMeetsTheRequirements(requiredQuests_pass, QuestStages.Passed)
            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MakeAvailableQuests(Quest[] quests)
    {
        foreach (var quest in quests)
            quest.ChangeStage(QuestStages.NotStarted);
    }

    private bool IsMeetsTheRequirements(Quest[] quests, QuestStages requiredStage)
    {
        foreach (var quest in quests)
            if (quest.stage != requiredStage)
                return false;

        return true;
    }

    #endregion
}