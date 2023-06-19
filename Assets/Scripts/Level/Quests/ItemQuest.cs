using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/ItemQuest", order = 0)]
public class ItemQuest : Quest
{
    #region Vars

    [Header("Quest Options")]
    public int countQuestAction;

    [Header("Special Replicas")]
    [TextArea(2, 4)]
    public string textGivingQuest;
    [TextArea(2, 4)]
    public string textNoDoneQuest;
    [TextArea(2, 4)]
    public string textCompleteQuest;

    [Header("Quest Item")]
    public Item questItem;
    public string endingPluralWord;

    #endregion

    public override bool QuestAvailability(int evilLevel)
    {
        if (stage != QuestStages.Passed && evilLevel <= availabilityLevel) { return true; }
        else { return false; }
    }

    public override void Initialize(Questor questor)
    {
        this.questor = questor;
    }

    public override void StartQuest()
    {
        EventHandler.OnQuestStart?.Invoke(this);
        EventHandler.OnReplicaSay?.Invoke(textGivingQuest + $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]");

        stage = QuestStages.Progressing;
    }

    public override void CompleteAction()
    {
        countQuestAction--;

        CheckConditions();
    }

    public override void CheckConditions()
    {
        if (countQuestAction <= 0)
        {
            countQuestAction = 0;

            if (!AttachedQuestIsAvailable())
            {
                stage = QuestStages.Completed;
            }
        }
    }

    private bool AttachedQuestIsAvailable()
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

    public override void ProgressingQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(
            textNoDoneQuest +
            $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]");
    }

    public override void CompleteQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(textCompleteQuest);

        PassedQuest();
    }

    public override void PassedQuest()
    {
        questor.ChangeSprite();
        stage = QuestStages.Passed;
        EventHandler.OnQuestPassed?.Invoke();
    }
}