using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/ItemQuest", order = 0)]
public class ItemQuest : Quest
{
    #region Vars

    [Header("Quest Options")]
    public ItemQuest prevQuest;
    public ItemQuest additionalQuest;
    public int evilLevelQuestAvailability;
    public int countQuestAction;

    [Header("Special Replicas")]
    public string textGivingQuest;
    public string textNoDoneQuest;
    public string textDoneQuest;

    [Header("Quest Item")]
    public Item questItem;
    public string endingPluralWord;

    [Header("State Questor")]
    [SerializeField] private QuestorStates _currentState;
    private enum QuestorStates
    { GiveQuest, NoDoneQuest, DoneQuest };

    #endregion

    #region Base Methods

    public override bool QuestAvailability(Questor questor, int evilLevel)
    {
        this.questor = questor;

        if (isActive && evilLevel <= evilLevelQuestAvailability)
        {
            _currentState = QuestorStates.GiveQuest;

            return true;
        }
        else { return false; }
    }

    public override void CompleteAction()
    {
        countQuestAction--;

        if (countQuestAction <= 0)
        {
            countQuestAction = 0;

            if (!AdditionalQuestIsActive())
            {
                _currentState = QuestorStates.DoneQuest;
            }
        }
    }

    public override void CheckQuest()
    {
        switch (_currentState)
        {
            case QuestorStates.GiveQuest:
                GiveQuest();
                break;
            case QuestorStates.NoDoneQuest:
                NoDoneQuest();
                break;
            case QuestorStates.DoneQuest:
                DoneQuest();
                break;
            default:
                DefaultState();
                break;
        }
    }

    #endregion

    #region Quest

    private void DefaultState()
    {
        questor.Greeting();
    }

    private bool AdditionalQuestIsActive()
    {
        if (additionalQuest != null && additionalQuest.isActive)
        {
            additionalQuest.prevQuest = this;

            return true;
        }
        else
        {
            return false;
        }
    }

    private void GiveQuest()
    {
        EventHandler.OnQuestStart?.Invoke(questName);

        var item = questItem;

        questor.Dialogue(textGivingQuest + $"\n[{item.nameItem}{endingPluralWord}: {countQuestAction}]");

        _currentState = QuestorStates.NoDoneQuest;
    }

    private void NoDoneQuest()
    {
        var item = questItem;

        questor.Dialogue(
            textNoDoneQuest +
            $"\n[{item.nameItem}{endingPluralWord}: {countQuestAction}]");
    }

    private void DoneQuest()
    {
        if (!AdditionalQuestIsActive())
        {
            questor.Dialogue(textDoneQuest);
            questor.PassedQuest();

            isActive = false;
        }
    }

    #endregion
}