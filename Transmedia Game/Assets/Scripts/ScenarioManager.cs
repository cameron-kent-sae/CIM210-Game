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
            scenarioTitleText.text = scenarios[0].scenarioTitle;
            scenarioDiscriptionText.text = scenarios[0].scenarioText;

            for(int i = 0; i < optionsButtons.Length; i++)
            {
                if(i == 0)
                {
                    optionsButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = scenarios[i].optionName;
                }
                else if (i == 1)
                {
                    //optionsButtons[1].gameObject.transform.GetChild("Text").GetComponent<Text>().text = scenarios[i].optionName;
                    Debug.Log(optionsButtons[1].gameObject.transform.childCount);
                }
                else if(i == 2)
                {
                    if (scenarios[0].hasOption3)
                    {
                        optionsButtons[i].gameObject.SetActive(true);

                        //optionsButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = scenarios[i].optionName;
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

                        //optionsButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = scenarios[i].optionName;
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
