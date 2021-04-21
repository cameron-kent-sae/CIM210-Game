using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButton : MonoBehaviour
{
     public ScenarioButton scenarioButton;

    [HideInInspector] public ScenarioManager scenarioManager;

    private void Start()
    {
        scenarioManager = GameObject.Find("ScenarioManager").GetComponent<ScenarioManager>();
    }

    public void ButtonSelected()
    {
        scenarioManager.PlayerOption(scenarioButton);
    }
}
