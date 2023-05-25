using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest", order = 0)]
public class Quest : DialogueQuest
{
    [Header("Special Replicas")]
    public string textGivingQuest;
    public string textNoDoneQuest;
    public string textDoneQuest;

    [Header("Quest Item")]
    public Item questItem;
    public string endingPluralWord;

    //private void ChangeState(QuestorStates currentState)
    //{
    //    switch (currentState)
    //    {
    //        case QuestorStates.Greeting:
    //            Greeting();
    //            break;
    //        case QuestorStates.GiveQuest:
    //            GiveQuest();
    //            break;
    //        case QuestorStates.NoDoneQuest:
    //            NoDoneQuest();
    //            break;
    //        case QuestorStates.DoneQuest:
    //            DoneQuest();
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private bool AdditionalQuestIsActive()
    //{
    //    if (_currentQuest.additionalQuest != null && _currentQuest.additionalQuest.isActive)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public void Dialogue(string text)
    //{
    //    _dialogText.text = text;
    //}

    //#region States

    //private void Greeting()
    //{
    //    if (_textGreeting != null)
    //    {
    //        Dialogue(_textGreeting);
    //    }

    //    IsGivingQuest();
    //}

    //private void GiveQuest()
    //{
    //    EventHandler.OnQuestStart?.Invoke(_currentQuest.questName);

    //    var item = _currentQuest.questItem;

    //    Dialogue(_currentQuest.textGivingQuest + $"\n[{item.nameItem}{_currentQuest.endingPluralWord}: {_currentQuest.countQuestAction}]");

    //    _currentState = QuestorStates.NoDoneQuest;
    //}

    //private void NoDoneQuest()
    //{
    //    var item = _currentQuest.questItem;

    //    Dialogue(
    //        _currentQuest.textNoDoneQuest +
    //        $"\n[{item.nameItem}{_currentQuest.endingPluralWord}: {_currentQuest.countQuestAction}]");
    //}

    //private void DoneQuest()
    //{
    //    if (!AdditionalQuestIsActive())
    //    {
    //        EventHandler.OnQuestPassed?.Invoke();

    //        _spriteRenderer.sprite = _smileNPC;
    //        _dialogText.text = _currentQuest.textDoneQuest;

    //        _currentQuest.isActive = false;

    //        _currentState = QuestorStates.Greeting;
    //        _currentQuest = null;
    //    }
    //}
}