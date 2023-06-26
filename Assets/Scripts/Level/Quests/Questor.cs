using UnityEngine.UI;
using UnityEngine;
using TMPro;
using InputData;
using EditorExtension;
using UnityEditor;
using System;
using System.IO;

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
    //[SerializeField] private Image _dialogBox;
    //[SerializeField] private TextMeshProUGUI _dialogText;
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

    //public void DialogBox(string text)
    //{
    //    _dialogText.text = text;
    //    _dialogBox.gameObject.SetActive(true);
    //    _dialogueReady = true;
    //}

    public void Dialogue()
    {
        //_dialogBox.gameObject.SetActive(false);

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
            _dialogueReady = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_gameMode == GameModes.Playing && collision.transform.tag == "Player")
            _dialogueReady = false;
    }

    #endregion

    #region Save & Load data Methods

    [Button("Save Quests")]
    public void SaveQuests() => Save();

    [Button("Save Quests With Replacement")]
    public void SaveQuestsWithReplacement() => Save(true);

    [Button("Load Quests")]
    public void LoadQuests()
    {
        foreach (var quest in _quests)
        {
            string path = $"Assets/Scriptable Objects/Quests/Quests Data/{questorName}";

            if (AssetDatabase.Contains(quest))
            {
                path += $"/{quest.questName}_copy.asset";

                CopyFields(AssetDatabase.LoadAssetAtPath(path, quest.GetType()), quest);
            }
        }
    }

    private void Save(bool withReplacement = false)
    {
        foreach (var quest in _quests)
        {
            string path = $"Assets/Scriptable Objects/Quests/Quests Data/{questorName}";

            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder(path, questorName);

            string assetName = $"/{quest.questName}_copy.asset";

            // Проверяем существование ассета.
            if (!withReplacement && File.Exists(path + assetName))
                throw new Exception($"Asset {assetName} already exists.");

            if (quest is ItemQuest)
            {
                var newQuest = ScriptableObject.CreateInstance<ItemQuest>();

                SaveAsset(quest, newQuest, path + assetName);
            }

            if (quest is DialogueQuest)
            {
                var newQuest = ScriptableObject.CreateInstance<DialogueQuest>();

                SaveAsset(quest, newQuest, path + assetName);
            }
        }
    }

    private void SaveAsset(UnityEngine.Object obj, UnityEngine.Object newObj, string path)
    {
        // Создаем ассет.
        AssetDatabase.CreateAsset(newObj, path);

        CopyFields(obj, newObj);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void CopyFields(UnityEngine.Object fromObj, UnityEngine.Object toObj)
    {
        // Находим все поля ассета.
        var fields = fromObj.GetType().GetFields();
        var newFields = toObj.GetType().GetFields();

        for (int i = 0; i < fields.Length; i++)
            // Устанавливаем для нового ассета newObj в поле newFields[i] значение fields[i] ассета obj.
            newFields[i].SetValue(toObj, fields[i].GetValue(fromObj));
    }

    #endregion
}