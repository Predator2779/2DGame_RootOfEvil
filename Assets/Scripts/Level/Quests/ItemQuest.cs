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

    public override void ProgressingQuest()
    {
        EventHandler.OnReplicaSay?.Invoke(
            textNoDoneQuest +
            $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]");
    }
}