using UnityEngine;

public class QuestButton : MonoBehaviour
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
            case Quest.QuestStages.Progressing:
                quest.ProgressingQuest();
                break;
            case Quest.QuestStages.Completed:
                quest.CompleteQuest();
                break;
        }
    }

    public void CloseDialogWindow()
    {
        EventHandler.OnDialogueWindowShow?.Invoke(false);
    }
}