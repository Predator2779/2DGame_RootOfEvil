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

    #region Base Methods

    public override bool QuestAvailability(Questor questor, int evilLevel)
    {
        this.questor = questor;

        return true;
    }

    public override void CompleteAction()
    {
        if (replicas.Length == 0)
        {
            isActive = false;
        }
    }

    public override void CheckQuest()
    {
        if (replicas.Length != 0)
        {
            if (randomSequence)
            {
                questor.Dialogue(GetRandomReplica());
            }
            else
            {
                questor.Dialogue(GetSequenceReplica());
            }
        }
        else
        {
            isActive = false;
        }
    }

    #endregion

    #region Quest

    private string GetRandomReplica()
    {
        int index = Random.Range(0, replicas.Length);
        string replica = replicas[index];

        replicas = RemoveIndex(replicas, index);

        return replica;
    } 
    
    private string GetSequenceReplica()
    {
        string replica = replicas[_currentReplicaIndex];

        replicas = RemoveIndex(replicas, _currentReplicaIndex);

        return replica;
    }

    private string[] RemoveIndex(string[] arr, int index)
    {
        for (int i = index + 1; i < arr.Length; i++)
        {
            arr[i - 1] = arr[i];
        }

        arr[arr.Length - 1] = null;

        string[] newArr = new string[arr.Length - 1];

        for (int i = 0; i < newArr.Length; i++)
        {
            newArr[i] = arr[i];
        }

        return arr;
    }

    #endregion
}