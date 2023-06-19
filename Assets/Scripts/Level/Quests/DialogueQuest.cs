using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/DialogueQuest", order = 0)]
public class DialogueQuest : Quest
{
    #region Vars

    [Header("Replicas")]
    public bool randomSequence = false;
    public string[] replicas;

    private int _currentReplicaIndex = 0;

    #endregion

    public override void ProgressingQuest()
    {
        
    }

    public override void CheckConditions()
    {
        if (replicas.Length <= 0 && !AttachedQuestIsAvailable())
        {
            CompleteQuest();
        }
        else
        {
            CompleteAction();
        }
    }

    public override void CompleteAction()
    {
        if (randomSequence)
        {
            EventHandler.OnReplicaSay?.Invoke(GetRandomReplica());
        }
        else
        {
            EventHandler.OnReplicaSay?.Invoke(GetSequenceReplica());
        }
    }

    #region Quest

    private string GetRandomReplica()
    {
        int index = Random.Range(0, replicas.Length);
        string replica = replicas[index];

        RemoveIndex(ref replicas, index);

        return replica;
    }

    private string GetSequenceReplica()
    {
        string replica = replicas[_currentReplicaIndex];

        RemoveIndex(ref replicas, _currentReplicaIndex);

        return replica;
    }

    private void RemoveIndex(ref string[] arr, int index)
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