using InputData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private ContentSizeFitter _content;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private GameObject _buttonTest;

    private void Start()
    {
        EventHandler.OnDialogueWindowShow.AddListener(WindowSetActive);
        EventHandler.OnShowQuests.AddListener(ShowQuests);
    }

    public void Update()
    {
        if (_dialogPanel.activeSelf == true && InputFunctions.GetEscapeButton_Up())
        {
            ClearDialogue();

            EventHandler.OnDialogueWindowShow?.Invoke(false);
            EventHandler.OnGameModeChanged?.Invoke(GameModes.Playing);
        }
    }

    public void WindowSetActive(bool value)
    {
        _dialogPanel.SetActive(value);
    }

    public void ShowQuests(Quest[] quests, Questor questor)
    {
        var dialogText = Instantiate(_dialogText, _content.transform);
        dialogText.text = "Список заданий:\n\n" + "\n\nEscape - закончить разговор.";

        foreach (Quest quest in quests)
        {
            SetButton(quest, questor);
        }
    }

    private void SetButton(Quest quest, Questor questor)
    {
        var clone = Instantiate(_buttonTest, parent: _content.transform);
        clone.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.description;

        var cloneQuest = clone.GetComponent<StartQuestButton>().quest = quest;
        cloneQuest.questor = questor;
    }

    public void ClearDialogue()
    {
        Transform[] allChildren = _content.transform.GetComponentsInChildren<Transform>();

        for(int i = 1; i < allChildren.Length; i++)
            Destroy(allChildren[i].gameObject);
    }
}