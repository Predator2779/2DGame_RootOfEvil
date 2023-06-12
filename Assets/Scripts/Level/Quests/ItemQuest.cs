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

    public override bool QuestAvailability(Questor questor, int evilLevel)
    {
        this.questor = questor;

        if (isAvailable && evilLevel <= availabilityLevel) { return true; }
        else { return false; }
    }

    public override void CompleteAction()
    {
        countQuestAction--;

        if (countQuestAction <= 0)
        {
            countQuestAction = 0;

            //if (!OptionalQuestIsAvailable())///to progressing
            //{
            //    currentState = QuestStates.Passed;
            //}
        }
    }

    public override void CheckConditions()
    {
        switch (currentState)
        {
            case QuestStates.NotStarted:
                StartQuest();
                break;
            case QuestStates.Progressing:
                ProgressingQuest();
                break;
            case QuestStates.Passed:
                PassedQuest();
                break;
            default:
                break;
        }
    }

    #endregion

    #region Quest

    private bool OptionalQuestIsAvailable()
    {
        if (optionalQuest != null && optionalQuest.isAvailable)
        {
            optionalQuest.parentQuest = this;

            return true;
        }
        else
        {
            return false;
        }
    }

    public override void StartQuest()
    {
        this.questor = questor;//

        EventHandler.OnQuestStart?.Invoke(this);

        if (OptionalQuestIsAvailable())
        {
            EventHandler.OnOptionalQuest?.Invoke(optionalQuest);
        }

        questor.Say(textGivingQuest + $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]");

        currentState = QuestStates.Progressing;
    }

    public void ProgressingQuest()
    {
        questor.Say(
            textNoDoneQuest +
            $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]");

        if (!OptionalQuestIsAvailable() && countQuestAction <= 0)
        {
            currentState = QuestStates.Passed;
        }
    }

    public override void PassedQuest()
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