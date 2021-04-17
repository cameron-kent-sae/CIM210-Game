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

    public GameObject[] optionsButtons;

    void Start()
    {
        LoadNextScenario();
        //UpdateScenarioOptions();
    }

    void Update()
    {
        
    }

    void UpdateScenarioOptions()
    {
        foreach(Scenario scenario in scenarios)
        {
            scenario.scenarioOptions = new ScenarioButton[4];

            for (int i = 0; i < 3; i++)
            {
                scenario.scenarioOptions[i] = new ScenarioButton();

                if (i == 0)
                    scenario.scenarioOptions[i].isTrue = true;
                else if (i == 1)
                    scenario.scenarioOptions[i].isTrue = true;
                else if (i == 2)
                    scenario.scenarioOptions[i].isTrue = scenario.hasOption3;
                else if (i == 3)
                    scenario.scenarioOptions[i].isTrue = scenario.hasOption4;
            }
        }
    }

    public void ButtonSelected(int number)
    {
        if(number == 0)
        {
            FinishScenario(scenarios[0].influenceLevel1, scenarios[0].conditionLevel1);
        }
        else if (number == 1)
        {
            FinishScenario(scenarios[0].influenceLevel2, scenarios[0].conditionLevel2);
        }
        else if (number == 2)
        {
            FinishScenario(scenarios[0].influenceLevel3, scenarios[0].conditionLevel3);
        }
        else if (number == 3)
        {
            FinishScenario(scenarios[0].influenceLevel4, scenarios[0].conditionLevel4);
        }
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

            for(int i = 0; i < optionsButtons.Length; i++)
            {
                if(i == 0)
                {
                    optionsButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = scenarios[0].optionName;
                }
                else if (i == 1)
                {
                    optionsButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = scenarios[0].option2Name;
                }
                else if(i == 2)
                {
                    if (scenarios[0].hasOption3)
                    {
                        optionsButtons[i].gameObject.SetActive(true);

                        optionsButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = scenarios[0].option3Name;
                    }
                    else
                    {
                        optionsButtons[i].gameObject.SetActive(false);
                    }
                }
                else if(i == 3)
                {
                    if (scenarios[0].hasOption4)
                    {
                        optionsButtons[i].gameObject.SetActive(true);

                        optionsButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = scenarios[0].option4Name;
                    }
                    else
                    {
                        optionsButtons[i].gameObject.SetActive(false);
                    }
                }

                // Set Text
            }
        }
        else
        {
            Debug.LogError("No scenarios in the list");
        }
    }
}
