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

    #region Base Methods

    public override void CompleteAction()
    {
        if (countQuestAction > 0)
            countQuestAction--;

        ConditionsIsDone();
    }

    public override void ProgressingQuest()
    {
        if (!ConditionsIsDone())
            EventHandler.OnReplicaSay?.Invoke(NoDoneReplica());
        else
            CompleteQuest();
    }

    public override bool ConditionsIsDone()
    {
        if (countQuestAction <= 0 && !AttachedQuestIsAvailable())
        {
            ChangeStage(QuestStages.Completed);

            return true;
        }
        else
            return false;
    }

    #endregion

    private string NoDoneReplica()
    {
        if (questItem != null)
            return noDoneReplicas[GetRandomIndex(noDoneReplicas)] +
                $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]";
        else
            return noDoneReplicas[GetRandomIndex(noDoneReplicas)];
    }
}