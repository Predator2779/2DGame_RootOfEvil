using UnityEngine;

public class StartQuestButton : MonoBehaviour
{
    public Quest quest;

    public void CheckStageQuest()
    {
        if (quest == null)
            return;

        switch (quest.stage)
        {
            case Quest.QuestStages.NotStarted:
                quest.StartQuest();
                break;
            case Quest.QuestStages.Progressing://
                break;
            case Quest.QuestStages.Completed:
                quest.CompleteQuest();
                break;
            case Quest.QuestStages.Passed:
                break;
        }
    }
}