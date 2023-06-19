using InputData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private ContentSizeFitter _replicaContent;
    [SerializeField] private ContentSizeFitter _questsContent;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private GameObject _startQuestButton;

    private void Start()
    {
        EventHandler.OnDialogueWindowShow.AddListener(WindowSetActive);
        EventHandler.OnShowQuests.AddListener(ShowQuests);
        EventHandler.OnReplicaSay.AddListener(Say);
    }

    public void Update()
    {
        if (_dialogPanel.activeSelf == true && InputFunctions.GetEscapeButton_Up())
        {
            ClearPanel(_questsContent.transform);
            ClearPanel(_replicaContent.transform);

            EventHandler.OnDialogueWindowShow?.Invoke(false);
            EventHandler.OnGameModeChanged?.Invoke(GameModes.Playing);
        }
    }

    public void WindowSetActive(bool value)
    {
        _dialogPanel.SetActive(value);
    }

    public void ShowQuests(Quest[] quests, Questor questor, int evilLevel)
    {
        ClearPanel(_questsContent.transform);

        Say("(Escape - закончить разговор)");

        var questText = Instantiate(_dialogText, _questsContent.transform);
        questText.text = "Список заданий:";

        foreach (Quest quest in quests)
        {
            if (quest.QuestAvailability(evilLevel))
            {
                SetButton(quest, questor);
            }
        }
    }

    private void SetButton(Quest quest, Questor questor)
    {
        var clone = Instantiate(_startQuestButton, parent: _questsContent.transform);
        clone.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.description;

        var cloneQuest = clone.GetComponent<StartQuestButton>().quest = quest;
        cloneQuest.Initialize(questor);
    }

    public void Say(string replica)
    {
        var replicaText = Instantiate(_dialogText, _replicaContent.transform);
        replicaText.text = replica;
    }

    public void ClearPanel(Transform panel)
    {
        Transform[] allChildren = panel.GetComponentsInChildren<Transform>();

        for(int i = 1; i < allChildren.Length; i++)
            Destroy(allChildren[i].gameObject);
    }
}