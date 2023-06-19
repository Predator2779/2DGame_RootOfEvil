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
    public Quest attachedQuest;

    [Header("Quest Launch Conditions")]
    public int availabilityLevel;
    public Quest[] requiredCompletedQuests;

    [Header("Quest States")]
    public QuestStages stage;
    public enum QuestStages
    { NotStarted, Progressing, Completed, Passed };

    public abstract bool QuestAvailability(int evilLevel);
    public abstract void Initialize(Questor questor);
    public abstract void StartQuest();
    public abstract void ProgressingQuest();
    public abstract void CompleteAction();
    public abstract void CheckConditions();
    public abstract void CompleteQuest();
    public abstract void PassedQuest();
}