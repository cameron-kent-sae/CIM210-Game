using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "Scenario/New Scenario")]

public class Scenario : ScriptableObject
{
    public string scenarioTitle = "New Scenario";

    [TextArea] public string scenarioText = "Scenario Text";

    [Header("Option 1")]
    [Header("Options")]
    public string optionName = "Yes";
    [Range(0, 100)] public int influenceLevel1 = 50;
    [Range(0, 100)] public int conditionLevel1 = 50;

    [Header("Option 2")]
    public string option2Name = "No";
    [Range(0, 100)] public int influenceLevel2 = 50;
    [Range(0, 100)] public int conditionLevel2 = 50;

    [Header("Option 3")]
    public bool hasOption3;
    public string option3Name = "Option 3";
    [Range(0, 100)] public int influenceLevel3 = 50;
    [Range(0, 100)] public int conditionLevel3 = 50;

    [Header("Option 4")]
    public bool hasOption4;
    public string option4Name = "Option 4";
    [Range(0, 100)] public int influenceLevel4 = 50;
    [Range(0, 100)] public int conditionLevel4 = 50;
}
