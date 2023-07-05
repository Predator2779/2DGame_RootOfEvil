using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EvilLevelCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _evilLevelCounter;
    [SerializeField] private PostProcessVolume _postProcessingVol;
    [SerializeField] private Transform _balk;
    [SerializeField] private string _textScales;
    [SerializeField][Range(0, 10)] private int _evilLevel;

    private void Start()
    {
        EventHandler.OnQuestPassed.AddListener(EvilLevelDown);
        EventHandler.OnEnemyKilled.AddListener(EvilLevelUp);
        EventHandler.OnEvilLevelChanged.Invoke(_evilLevel);
    }

    public int GetCurrentEvilLevel() => _evilLevel;

    private void EvilLevelUp()
    {
        if (_evilLevel < 10)
            _evilLevel++;

        EvilLevelChange();
    }

    private void EvilLevelDown(Quest quest)
    {
        if (_evilLevel > 0)
            _evilLevel--;

        EvilLevelChange();
    }

    private void EvilLevelChange()
    {
        SetScalesAngle();

        _evilLevelCounter.text = $"{_textScales}: {_evilLevel}";

        EventHandler.OnEvilLevelChanged.Invoke(_evilLevel);
    }

    private void SetScalesAngle()
    {
        _postProcessingVol.weight = (float)_evilLevel / 10;
        _balk.transform.rotation = Quaternion.Euler(0, 0, (5 - _evilLevel) * 5);
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