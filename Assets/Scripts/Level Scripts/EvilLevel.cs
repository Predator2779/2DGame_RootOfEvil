using TMPro;
using UnityEngine;

public class EvilLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _evilLevelCounter;

    [SerializeField][Range(1, 10)] private int _evillLevel;

    private void Start()
    {
        EventHandler.OnQuestPassed.AddListener(EvilLevelDown);
        EventHandler.OnEvilLevelChanged.Invoke(_evillLevel);
    }

    private void EvilLevelUp()
    {
        _evillLevel++;
        
        EvilLevelChange();
    }  
    
    private void EvilLevelDown()
    {
        _evillLevel--;

        EvilLevelChange();
    }

    private void EvilLevelChange()
    {
        _evilLevelCounter.text = $"������� ������� ���: {_evillLevel}";

        EventHandler.OnEvilLevelChanged.Invoke(_evillLevel);
    }

    #region EDITOR_MODE

    private int _oldEvilLevel;

    private void Update()
    {
        CheckLevel();
    }

    private void CheckLevel()
    {
        if (_evillLevel != _oldEvilLevel)
        {
            EvilLevelChange();

            _oldEvilLevel = _evillLevel;
        }
    }

    #endregion
}