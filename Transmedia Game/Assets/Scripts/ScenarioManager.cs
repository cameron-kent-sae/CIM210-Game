using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public List<Scenario> scenarios;

    public PlayerStats playerStats;

    public AIManager aiManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ButtonSelected(float influence, float condition)
    {
        if (aiManager)
        {
            aiManager.UpdateAiStats(influence, condition);
        }
        else
        {
            Debug.LogError("Missing aiManager");
        }


    }

    void LoadNextScenario()
    {
        if(scenarios.Count > 0)
        {
            // Do Stuff
        }
        else
        {
            Debug.LogError("No scenarios in the list");
        }
    }
}
