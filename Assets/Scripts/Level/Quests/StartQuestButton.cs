using UnityEngine;

public class StartQuestButton : MonoBehaviour
{
    public Quest quest;

    public void StartQuest()
    {
        if (quest != null)
            quest.StartQuest();
    }
}