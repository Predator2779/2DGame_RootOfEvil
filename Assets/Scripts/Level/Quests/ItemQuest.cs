using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/ItemQuest", order = 0)]
public class ItemQuest : Quest
{
    #region Vars

    [Header("Quest Options")]
    public int countQuestAction;

    [Header("Quest Item")]
    public Item questItem;
    public string endingPluralWord;

    #endregion

    public override void CompleteAction()
    {
        countQuestAction--;

        if (countQuestAction <= 0)
        {
            countQuestAction = 0;
        }

        ConditionsIsDone();
    }


    public override void ProgressingQuest()
    {
        if (!ConditionsIsDone())
        {
            EventHandler.OnReplicaSay?.Invoke(NoDoneReplica());
        }
        else
        {
            CompleteQuest();
        }
    }

    public override bool ConditionsIsDone()
    {
        if (countQuestAction <= 0 && !AttachedQuestIsAvailable())
        {
            stage = QuestStages.Completed;

            return true;
        }
        else
        {
            return false;
        }
    }

    private string NoDoneReplica()
    {
        if (questItem != null)
        {
            return textNoDoneQuest + $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]";
        }
        else
        {
            return textNoDoneQuest;
        }
    }
}