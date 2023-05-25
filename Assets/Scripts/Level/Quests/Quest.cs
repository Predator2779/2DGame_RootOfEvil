using System;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    [Header("Quest")]
    public bool isActive;
    public string questName;
    public string description;
    [NonSerialized] public Questor questor;

    public abstract bool QuestAvailability(Questor questor, int evilLevel);
    public abstract void CheckQuest();
    public abstract void CompleteAction();
}