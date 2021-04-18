using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Stats", menuName = "Character Stats/AI Stats")]

public class AIStats : ScriptableObject
{
    public string characterName = "Unnamed Character";

    public Sprite characterSprite;

    public float maxInfluenceThreshold = 15;
    public float maxConditionThreshold = 15;

    [Header("Favor Bias")]
    [Range(-100, 100)] public float influenceUpConditionUp;
    [Range(-100, 100)] public float influenceDownConditionUDown;
    [Range(-100, 100)] public float influenceUpConditionDown;
    [Range(-100, 100)] public float influenceDownConditionUp;
}
