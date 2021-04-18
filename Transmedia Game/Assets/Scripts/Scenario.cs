using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "Scenario/New Scenario")]

public class Scenario : ScriptableObject
{
    public string scenarioTitle = "New Scenario";

    [TextArea] public string scenarioText = "Scenario Text";

    public ScenarioButton[] scenarioButtons;

}
