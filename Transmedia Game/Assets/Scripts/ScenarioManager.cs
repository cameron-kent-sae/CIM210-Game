using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenarioManager : MonoBehaviour
{
    [Header("Scenario")]
    public List<Scenario> scenarios;

    public AIManager aiManager;

    public int winConditionLevel = 100;
    public int loseConditionLevel = 0;

    [Header("Player")]
    public CharacterStats playerStats;

    public int startingInfluence = 0;
    public int maxInfluence = 100;
    public float startingCondition = 0;
    private float condition = 50;

    [Header("UI")]
    public Image influenceSlider;
    public Image conditionSlider;

    [Header("Buttons")]
    //public GameObject buttonsPrefab;
    public GameObject[] buttons;

    [Header("Text")]
    public TMP_Text scenarioTitleText;
    public TMP_Text scenarioDiscriptionText;
    public TMP_Text endingTitleText;
    public TMP_Text endingDiscriptionText;

    private ScenarioButton playerButtonChoice;

    private List<GameObject> uIButtons;

    void Start()
    {
        uIButtons = new List<GameObject>();

        LoadNextScenario();
        SetStartingStats();
    }

    void SetStartingStats()
    {
        playerStats.influence = startingInfluence;

        condition = startingCondition;
    }

    public void AIChoice(ScenarioButton button, int chance)
    {
        Debug.Log("Scenario Manager: AIChoice, button: " + button + ", chance: " + chance);

        button.voteCount += chance;
    }

    public void PlayerOption(ScenarioButton button)
    {
        Debug.Log("Scenario Manager: Player Option, button: " + button);

        AIChoice(button, playerStats.influenceMultiplier);

        aiManager.GenerateChoices(scenarios[0]);

        playerButtonChoice = button;
    }

    public void GenerateScenarioOutcome()
    {
        uIButtons.Sort(SortScenarioOptionScores);

        FinishScenario(uIButtons[uIButtons.Count - 1].GetComponent<CustomButton>().scenarioButton);
    }

    int SortScenarioOptionScores(GameObject button1, GameObject button2)
    {
        return button1.GetComponent<CustomButton>().scenarioButton.voteCount.CompareTo(button2.GetComponent<CustomButton>().scenarioButton.voteCount);
    }

    void FinishScenario(ScenarioButton button)
    {
        Debug.Log("Scenario Manager: Finish Scenario, Button: " + button);

        if (playerButtonChoice == button)
        {
            playerStats.influence += button.baseInfluence;

            if(playerStats.influence > maxInfluence)
            {
                playerStats.influence = maxInfluence;
            }
        }
        else
        {
            playerStats.influence -= 15;
        }

        condition += button.baseCondition;

        aiManager.UpdateAiStats(button);

        scenarios.RemoveAt(0);

        foreach (GameObject uiButton in uIButtons)
        {
            uiButton.GetComponent<CustomButton>().scenarioButton.voteCount = 0;

            uiButton.SetActive(false);
        }

        uIButtons.Clear();

        UpdateUISliders();
        CheckForWin();
    }

    void CheckForWin()
    {
        if (condition >= winConditionLevel)
        {
            string winTitle = "Condition Win";
            string winText = "The world condition has drastically improved and now everyone lives in an equal and peiceful world.";

            EndGame(winTitle, winText);
        }
        else if (condition <= loseConditionLevel)
        {
            string loseTitle = "Condition Lose";
            string loseText = "The world condition has drastically decreced and now the world has gone to shit. Good job asshole.   -sorry I'll change this text later";

            EndGame(loseTitle, loseText);
        }
        else if (playerStats.influence <= -100)
        {
            string loseTitle = "Influence Lose";
            string loseText = "Your influence has dropped so much that no one likes you anymore and you have been kicked out. You can no longer vote on policies and have lost the power to change the world.";

            EndGame(loseTitle, loseText);
        }
        else
        {
            LoadNextScenario();
        }
    }

    void UpdateUISliders()
    {
        if (influenceSlider)
        {
            influenceSlider.fillAmount = (playerStats.influence + 100) / 200;
        }
        else
        {
            Debug.LogError("Missing influence slider");
        }

        if (conditionSlider)
        {
            conditionSlider.fillAmount = condition / 100;
        }
        else
        {
            Debug.LogError("Missing condition slider");
        }
    }

    void LoadNextScenario()
    {
        Debug.Log("Load next Scenario");
        Debug.Log("Scenario Manager: Scenarios Count:" + scenarios.Count);

        if (scenarios.Count > 0)
        {            
            playerButtonChoice = null;

            scenarioTitleText.text = scenarios[0].scenarioTitle;
            scenarioDiscriptionText.text = scenarios[0].scenarioText;

            if (buttons.Length == 4)
            {
                for (int i = 0; i < scenarios[0].scenarioButtons.Length; i++)
                {
                    //GameObject button = Instantiate(buttonsPrefab, buttonTransforms[i], buttonTransforms[i]);
                    //button.transform.position = buttonTransforms[i].position;

                    //Debug.Log("Scenario Manager: Scenario Buttons current button:" + scenarios[0].scenarioButtons[i] + " / " + scenarios[0].scenarioButtons.Length);

                    buttons[i].SetActive(true);

                    //buttons[i].GetComponentInChildren<TMP_Text>().text = scenarios[0].scenarioButtons[i].buttonText;

                    uIButtons.Add(buttons[i]);

                    buttons[i].name = "Button " + i;

                    if (buttons[i].GetComponent<CustomButton>())
                    {
                        buttons[i].GetComponent<CustomButton>().scenarioButton = scenarios[0].scenarioButtons[i];
                        buttons[i].GetComponent<CustomButton>().scenarioButton.voteCount = 0;
                        buttons[i].GetComponent<CustomButton>().scenarioManager = gameObject.GetComponent<ScenarioManager>();
                    }
                    else
                    {
                        Debug.LogError("Button missing CustomButton Component");
                    }
                }

                for(int i = scenarios[0].scenarioButtons.Length; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(false);
                }
            }
            else
            {
                Debug.LogError("Missing buttons, current number of buttons = " + buttons.Length);
            }
        }
        else
        {
            Debug.Log("No scenarios in the list");

            string endingTitle = "Time Lose";
            string endingText = "The world remains stagnate with it's condition neither drastically improving nor deteriorating";

            EndGame(endingTitle, endingText);
        }
    }

    public void EndGame(string endingTitle, string endingText)
    {
        if(endingTitleText || endingDiscriptionText)
        {
            endingTitleText.gameObject.SetActive(true);
            endingDiscriptionText.gameObject.SetActive(true);

            endingTitleText.text = endingTitle;
            endingDiscriptionText.text = endingText;
        }
        else
        {
            Debug.LogError("Missing ending discription text or ending title text.");
        }
    }
}
