using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/DialogueQuest", order = 0)]
public class DialogueQuest : Quest
{
    #region Vars

    [Header("Quest Replicas")]
    public bool randomSequence = false;
    public string[] questReplicas;

    #endregion

    #region Base Methods

    public override void CompleteAction()
    {
        int index;

        if (randomSequence)
        {
            index = GetRandomIndex(questReplicas);
            EventHandler.OnReplicaSay?.Invoke(questor, questReplicas[index]);
        }
        else
        {
            index = 0;
            EventHandler.OnReplicaSay?.Invoke(questor, questReplicas[index]);
        }

        RemoveReplica(ref questReplicas, index);
    }

    public override void NoDone()
    {
        CompleteAction();
    }

    public override bool SomeCondition()
    {
        return questReplicas.Length <= 0;
    }

    #endregion

    #region Other Methods

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