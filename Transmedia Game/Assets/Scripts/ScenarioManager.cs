using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    public List<Scenario> scenarios;

    public PlayerStats playerStats;

    public AIManager aiManager;

    public Text scenarioTitleText;
    public Text scenarioDiscriptionText;

    public GameObject buttonsPrefab;
    public Transform[] buttonTransforms;

    void Start()
    {
        LoadNextScenario();
        //UpdateScenarioOptions();
    }

    void Update()
    {
        
    }

    void FinishScenario(float influence, float condition)
    {
        //playerStats
    }

    void LoadNextScenario()
    {
        if(scenarios.Count > 0)
        {
            scenarioTitleText.text = scenarios[0].scenarioTitle;
            scenarioDiscriptionText.text = scenarios[0].scenarioText;

            if(buttonTransforms.Length > 0)
            {
                if (buttonsPrefab)
                {
                    for(int i = 0; i < scenarios[0].scenarioButtons.Length; i++)
                    {
                        GameObject button = Instantiate(buttonsPrefab, buttonTransforms[i], buttonTransforms[i]);
                        button.transform.position = buttonTransforms[i].position;
                        button.GetComponentInChildren<Text>().text = scenarios[0].scenarioButtons[i].buttonText;
                    }
                }
                else
                {
                    Debug.LogError("Missing Button Prefab");
                }
            }
            else
            {
                Debug.LogError("No Button Transforms");
            }
        }
        else
        {
            Debug.LogError("No scenarios in the list");
        }
    }
}
