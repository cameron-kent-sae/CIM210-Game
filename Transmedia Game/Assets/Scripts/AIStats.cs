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

    [Tooltip("Closer to 0 is condition, closer to 100 is influence")][Range(0, 100)] public float conditionVsInfluence = 50;
}
