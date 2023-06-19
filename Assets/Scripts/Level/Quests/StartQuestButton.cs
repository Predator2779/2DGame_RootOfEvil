using UnityEngine;

public class StartQuestButton : MonoBehaviour
{
    public Quest quest;

    public void CheckStageQuest()
    {
        if (quest == null)
            return;

        quest.CheckConditions();

        switch (quest.stage)
        {
            case Quest.QuestStages.NotStarted:
                quest.StartQuest();
                break;
            case Quest.QuestStages.Progressing:
                quest.ProgressingQuest();
                break;
            case Quest.QuestStages.Completed:
                quest.CompleteQuest();
                break;
        }
    }
}