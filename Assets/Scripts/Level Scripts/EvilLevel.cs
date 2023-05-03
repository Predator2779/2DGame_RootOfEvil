using TMPro;
using UnityEngine;

public class EvilLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _evilLevelCounter;

    [SerializeField][Range(1, 10)] private int _evillLevel;

    private void Start()
    {
        EventHandler.OnEvilLevelChanged.AddListener(ChangeEvilLevel);
    }

    private void ChangeEvilLevel(int value)
    {
        _evillLevel += value;

        _evilLevelCounter.text = $"Текущий уровень зла: {_evillLevel}";
    }
}