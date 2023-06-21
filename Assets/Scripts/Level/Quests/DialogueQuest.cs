using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/DialogueQuest", order = 0)]
public class DialogueQuest : Quest
{
    #region Vars

    [Header("Replicas")]
    public bool randomSequence = false;
    public string[] replicas;

    #endregion

    #region Base Methods

    public override bool ConditionsIsDone()
    {
        if (replicas.Length > 0)
        {
            CompleteAction();

            return false;
        }
        else
        {
            if (AttachedQuestIsAvailable())
            {
                EventHandler.OnReplicaSay?.Invoke(noDoneReplicas[GetRandomIndex(noDoneReplicas)]);

                return false;
            }
            else
            {
                CompleteQuest();

                return true;
            }
        }
    }

    public override void ProgressingQuest()
    {
        ConditionsIsDone();
    }

    public override void CompleteAction()
    {
        int index;

        if (randomSequence)
        {
            index = GetRandomIndex(replicas);
            EventHandler.OnReplicaSay?.Invoke(replicas[index]);
        }
        else
        {
            index = 0;
            EventHandler.OnReplicaSay?.Invoke(replicas[index]);
        }

        RemoveReplica(ref replicas, index);
    }

    //public override void PassedQuest()
    //{
    //    questor.ChangeSprite();
    //    stage = QuestStages.Passed;

    //    EventHandler.OnQuestPassed?.Invoke(this);
    //}

    #endregion

    #region Quest

    private void RemoveReplica(ref string[] arr, int index)
    {
        for (int i = index + 1; i < arr.Length; i++)
            arr[i - 1] = arr[i];

        string[] newArr = new string[arr.Length - 1];

        for (int i = 0; i < newArr.Length; i++)
            newArr[i] = arr[i];

        arr = newArr;
    }

    #endregion
}