using System;
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

    public override bool SomeCondition()
    {
        return countQuestAction <= 0 ? true : false;
    }

    public override void NoDone()
    {
        EventHandler.OnReplicaSay?.Invoke(questor, NoDoneReplica());
    }

    #endregion

    #region Other Methods

    private string NoDoneReplica()
    {
        if (questItem != null)
            return noDoneReplicas[GetRandomIndex(noDoneReplicas)] +
                $"\n[{questItem.nameItem}{endingPluralWord}: {countQuestAction}]";
        else
            return noDoneReplicas[GetRandomIndex(noDoneReplicas)];
    }

    #endregion
}