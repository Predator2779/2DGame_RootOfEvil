using UnityEngine.UI;
using UnityEngine;
using TMPro;
using InputData;
using EditorExtension;
using UnityEditor;
using Unity.VisualScripting;

public class Questor : MonoBehaviour
{
    [Header("Questor")]
    public string questorName;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _smileNPC;
    [SerializeField] private Sprite _sadNPC;

    [Header("EvilLevel")]
    [SerializeField] private EvilLevelCounter _evilLevelCounter;

    [Header("Dialogue")]
    [SerializeField] private Image _dialogBox;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [TextArea(2, 4)]
    [SerializeField] private string _textGreeting = "Приветствую!";

    [Header("Quest")]
    [SerializeField] private Quest[] _quests;

    private bool _dialogueReady = false;
    private GameModes _gameMode;

    private void Start()
    {
        EventHandler.OnEvilLevelChanged.AddListener(SetSadSprite);

        if (_spriteRenderer == null)
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = _sadNPC;
    }

    private void Update()
    {
        if (InputFunctions.GetKeyF_Up() && _dialogueReady)
            Dialogue();
    }

    public void Say(string text)
    {
        _dialogText.text = text;
        _dialogBox.gameObject.SetActive(true);
        _dialogueReady = true;
    }

    public void Dialogue()
    {
        _dialogBox.gameObject.SetActive(false);

        EventHandler.OnGameModeChanged?.Invoke(GameModes.Pause);
        EventHandler.OnDialogueWindowShow?.Invoke(true);
        EventHandler.OnShowQuests?.Invoke
            (_quests, this, _evilLevelCounter.GetCurrentEvilLevel());
        EventHandler.OnReplicaSay?.Invoke(this, _textGreeting);
    }

    private void OnEnable() => EventHandler.OnGameModeChanged.AddListener(ChangeGameMode);

    private void OnDisable() => EventHandler.OnGameModeChanged.RemoveListener(ChangeGameMode);

    private void ChangeGameMode(GameModes mode) => _gameMode = mode;

    public void SetSmileSprite() => _spriteRenderer.sprite = _smileNPC;

    public void SetSadSprite(int evilLevel)
    {
        if (evilLevel >= 9)
            _spriteRenderer.sprite = _sadNPC;
    }

    #region Trigger

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
            Say("[F] - поговорить.");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
        {
            _dialogueReady = false;
            _dialogBox.gameObject.SetActive(false);
        }
    }

    [Button("Save Quests")]
    public void SaveQuests()
    {
        foreach (var quest in _quests)
        {
            if (quest.GetComponent<ItemQuest>())
            {
                SaveItemQuest(quest.GetComponent<ItemQuest>());
            }

            if (quest.GetComponent<DialogueQuest>())
            {
                SaveDialogueQuest(quest.GetComponent<DialogueQuest>());
            }
        }
    }

    private void SaveItemQuest(ItemQuest quest)
    {
        var newQuest = ScriptableObject.CreateInstance<ItemQuest>();
        string path = $"Assets/Scriptable Objects/Quests/QuestsData/{questorName}/{quest.questName}.asset";

        CopyObjectFields(quest, newQuest, path);
    }

    private void SaveDialogueQuest(DialogueQuest quest)
    {
        var newQuest = ScriptableObject.CreateInstance<DialogueQuest>();
        string path = $"Assets/Scriptable Objects/Quests/QuestsData/{quest.questName}.asset";

        CopyObjectFields(quest, newQuest, path);
    }

    private void CopyObjectFields(Object obj, Object newObj, string path)
    {
        AssetDatabase.CreateAsset(obj, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = obj;

        var fields = obj.GetType().GetFields();
        var newFields = newObj.GetType().GetFields();

        for (int i = 0; i < fields.Length; i++)
        {
            newFields[i] = fields[i];
        }
    }

    #endregion
}