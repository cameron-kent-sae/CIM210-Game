using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario Button", menuName = "Scenario/Scenario Button")]

public class ScenarioButton : ScriptableObject
{
    public string buttonText;
    [Range(-50, 50)] public float baseInfluence;
    [Range(-50, 50)] public float baseCondition;

    [HideInInspector] public int voteCount;
}
