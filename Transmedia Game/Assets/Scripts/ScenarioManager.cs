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

    private List<ScenarioButton> scenarioButtons;
    private List<float> scenarioButtonChances;

    private ScenarioButton playerButtonChoice;

    void Start()
    {
        LoadNextScenario();
    }

    public void AIChoice(ScenarioButton button, float chance)
    {
        scenarioButtons.Add(button);
        scenarioButtonChances.Add(chance);
    }

    public void PlayerOption(ScenarioButton button)
    {
        AIChoice(button, playerStats.influenceLevel);

        aiManager.GenerateChoices(scenarios[0]);

        playerButtonChoice = button;
    }

    public void GenerateScenarioOutcome()
    {
        float totalSoundWeighting = 0;

        foreach (float chance in scenarioButtonChances)
        {
            totalSoundWeighting += chance;
        }

        float randomNumber = Random.Range(1, totalSoundWeighting);
        float counter = 0;

        for (int i = 0; i < scenarioButtonChances.Count; i++)
        {
            if (randomNumber > counter && randomNumber < counter + scenarioButtonChances[i])
            {
                FinishScenario(scenarioButtons[i]);
            }

            counter += scenarioButtonChances[i];
        }
    }

    void FinishScenario(ScenarioButton button)
    {
        if(playerButtonChoice == button)
        {
            playerStats.influenceLevel += button.baseInfluence;
        }
        else
        {
            playerStats.influenceLevel -= 15;
        }

        playerStats.conditionLevel += button.baseCondition;

        aiManager.UpdateAiStats(button);
        
        LoadNextScenario();
    }

    void LoadNextScenario()
    {
        if(scenarios.Count > 0)
        {
            scenarioButtons.Clear();
            scenarioButtonChances.Clear();

            playerButtonChoice = null;

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
