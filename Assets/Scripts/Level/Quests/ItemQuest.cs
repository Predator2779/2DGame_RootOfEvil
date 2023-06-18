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
    public string textDoneQuest;

    [Header("Quest Item")]
    public Item questItem;
    public string endingPluralWord;

    #endregion

    #region Base Methods

    public override bool QuestAvailability(int evilLevel)
    {
        if (stage == QuestStages.NotStarted && evilLevel <= availabilityLevel) { return true; }
        else { return false; }
    }

    public override void Initialize(Questor questor)
    {
        this.questor = questor;
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

    #endregion

    #region Quest

    private bool AttachedQuestIsAvailable()
    {
        if (attachedQuest != null && attachedQuest.stage != QuestStages.Completed)
        {
            //attachedQuest.parentQuest = this;

            return true;
        }
        else
        {
            return false;
        }
    }

    public override void StartQuest()
    {
        EventHandler.OnQuestStart?.Invoke(this);

        if (AttachedQuestIsAvailable())
        {
            EventHandler.OnOptionalQuest?.Invoke(attachedQuest);
        }

        questor.Say(textGivingQuest + $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]");

        stage = QuestStages.Progressing;
    }

    public void ProgressingQuest()
    {
        questor.Say(
            textNoDoneQuest +
            $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]");

        //if (!AttachedQuestIsAvailable() && countQuestAction <= 0)
        //{
        //    stage = QuestStages.Passed;
        //}
    }

    public override void CompleteQuest()
    {
        if (parentQuest != null)
        {
            parentQuest.CompleteAction();
        }

        questor.Say(textDoneQuest);
        questor.ChangeSprite();

        isAvailable = false;

        EventHandler.OnQuestPassed?.Invoke();
    }

    #endregion
}