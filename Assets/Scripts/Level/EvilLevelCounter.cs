using TMPro;
using UnityEngine;

public class EvilLevelCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _evilLevelCounter;
    [SerializeField] private string _textScales;
    [SerializeField][Range(1, 10)] private int _evilLevel;

    private void Start()
    {
        EventHandler.OnQuestPassed.AddListener(EvilLevelDown);
        EventHandler.OnEnemyKilled.AddListener(EvilLevelUp);
        EventHandler.OnEvilLevelChanged.Invoke(_evilLevel);
    }

    public int GetCurrentEvilLevel()
    {
        return _evilLevel;
    }

    private void EvilLevelUp()
    {
        _evilLevel++;
        
        EvilLevelChange();
    }  
    
    private void EvilLevelDown()
    {
        _evilLevel--;

        EvilLevelChange();
    }
    
    private void EvilLevelDown(Quest quest)//
    {
        _evilLevel--;

        EvilLevelChange();
    }

    private void EvilLevelChange()
    {
        _evilLevelCounter.text = $"{_textScales}: {_evilLevel}";

        EventHandler.OnEvilLevelChanged.Invoke(_evilLevel);
    }

    #region EDITOR_MODE

    private int _oldEvilLevel;

    private void Update()
    {
        CheckLevel();
    }

    private void CheckLevel()
    {
        if (_evilLevel != _oldEvilLevel)
        {
            EvilLevelChange();

            _oldEvilLevel = _evilLevel;
        }
    }

    #endregion
}