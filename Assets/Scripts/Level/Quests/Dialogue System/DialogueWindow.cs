using InputData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField]private Questor questor;
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private ContentSizeFitter _replicaContent;
    [SerializeField] private ContentSizeFitter _questsContent;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private Button _questButton;
    [SerializeField] private Button _closeDialogButton;

    private void Start()
    {
        EventHandler.OnDialogueWindowShow.AddListener(WindowSetActive);
        EventHandler.OnShowQuests.AddListener(ShowQuests);
        EventHandler.OnReplicaSay.AddListener(Say);
    }

    public void Update()
    {
        if (_dialogPanel.activeSelf == true && InputFunctions.GetEscapeButton_Up())
            WindowSetActive(false);
    }

    public void WindowSetActive(bool value)
    {
        _dialogPanel.SetActive(value);

        if (value == false)
            CloseDialogWindow();
    }

    public void ShowQuests(Quest[] quests, Questor questor, int evilLevel)
    {
        this.questor = questor;

        ClearPanel(_questsContent.transform);

        var questText = Instantiate(_dialogText, _questsContent.transform);
        questText.text = "Список заданий:";

        foreach (Quest quest in quests)
            if (quest.QuestAvailability(evilLevel))
                SetQuestButton(quest, questor);

        SetQuitDialogButton();
    }

    private void SetQuestButton(Quest quest, Questor questor)
    {
        var clone = SetButton(_questButton, _questsContent.transform);
        clone.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.description;

        var cloneQuest = clone.GetComponent<QuestButton>().quest = quest;
        cloneQuest.Initialize(questor);
    }

    private void SetQuitDialogButton()
    {
        var clone = SetButton(_closeDialogButton, _questsContent.transform);
        clone.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Закончить разговор.";
    }

    private Button SetButton(Button button, Transform parent)
    {
        return Instantiate(button, parent: parent);
    }

    public void Say(Questor questor, string replica)
    {
        if (this.questor != null && questor == this.questor)
        {
            var replicaText = Instantiate(_dialogText, _replicaContent.transform);
            replicaText.text = replica;
        }
    }

    public void ClearPanel(Transform panel)
    {
        Transform[] allChildren = panel.GetComponentsInChildren<Transform>();

        for (int i = 1; i < allChildren.Length; i++)
            Destroy(allChildren[i].gameObject);
    }

    public void CloseDialogWindow()
    {
        ClearPanel(_questsContent.transform);
        ClearPanel(_replicaContent.transform);

        EventHandler.OnGameModeChanged?.Invoke(GameModes.Playing);
    }
}