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
    [Tooltip("�� ������, ���� ����� ����� ����� ���������� �������� � ����������, ����� �� �� �����������")]
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
    [Tooltip("��������� ������� ��� � ���� (���� ����������)")]
    public int availabilityLevel = 10;
    [Tooltip("��� ������ ������ ���� ������, ��� ���� ����� ����� ���� ���������")]
    public Quest[] requiredQuests_start;
    [Tooltip("��� ������ ������ ���� ���������, ��� ���� ����� ����� ���� ���������")]
    public Quest[] requiredQuests_complete;
    [Tooltip("��� ������ ������ ���� ���������, ��� ���� ����� ����� ���� ���������")]
    public Quest[] requiredQuests_pass;

    [Header("Change other Quests")]
    [Tooltip("������, ����������� ������ ����� ����� �� ������")]
    public Quest[] passQuests_start;
    [Tooltip("������, ����������� ������ ����� ����� �� ����������")]
    public Quest[] passQuests_complete;
    [Tooltip("������, ����������� ������ ����� ����� �� ����������")]
    public Quest[] passQuests_pass;

    [Header("Make Available after this Quests")]
    [Tooltip("������ ���������� ��� ������, ����� ������ �����")]
    public Quest[] availableQuests_start;
    [Tooltip("������ ���������� ��� ������, ����� ���������� �����")]
    public Quest[] availableQuests_complete;
    [Tooltip("������ ���������� ��� ������, ����� ���������� �����")]
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