using System;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    [Header("Quest")]
    public bool isAvailable;
    public string questName;
    public string description;

    [Header("Quest Options")]
    [NonSerialized] public Questor questor;
    [NonSerialized] public Quest parentQuest;
    public Quest optionalQuest;
    public int availabilityLevel;

    [Header("Quest States")]
    public QuestStates currentState;
    public enum QuestStates
    { NotStarted, Progressing, Passed };

    public abstract bool QuestAvailability(Questor questor, int evilLevel);
    public abstract void CompleteAction();
    public abstract void CheckQuest();
    public abstract void PassedQuest();
}