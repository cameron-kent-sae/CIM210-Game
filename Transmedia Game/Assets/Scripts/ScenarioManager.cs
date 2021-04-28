﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScenarioManager : MonoBehaviour
{
    [Header("Scenario")]
    public List<Scenario> scenarios;

    public AIManager aiManager;

    public int winConditionLevel = 100;
    public int loseConditionLevel = 0;

    [Header("Player")]
    public CharacterStats playerStats;

    public int maxInfluence = 100;
    public float startingCondition = 50;
    private float condition = 50;

    [Header("UI")]
    public Image influenceSlider;
    public Image conditionSlider;

    [Header("Counting Votes UI")]
    public GameObject countingVotesUIPanel;
    public GameObject countingVotesUI;
    public GameObject votePassedUI;
    public TMP_Text votePassedText;
    private ScenarioButton passedVote;

    [Header("Buttons")]
    public GameObject[] buttons;
    private ScenarioButton playerButtonChoice;
    private List<GameObject> uIButtons;

    [Header("Text")]
    public TMP_Text scenarioTitleText;
    public TMP_Text scenarioDiscriptionText;

    private string winScene = "Win";
    private string loseScene = "Lose";

    void Start()
    {
        uIButtons = new List<GameObject>();

        //LoadNextScenario();
        Invoke("LoadNextScenario", 0.1f);
        SetStartingStats();
    }

    void SetStartingStats()
    {
        playerStats.influence = playerStats.startingInfluence;

        condition = startingCondition;
    }

    public void AIChoice(ScenarioButton button, int chance)
    {
        Debug.Log("Scenario Manager: AIChoice, button: " + button + ", chance: " + chance);

        button.voteCount += chance;
    }

    public void PlayerOption(ScenarioButton button)
    {
        Debug.Log("Scenario Manager: Player Input Button, button: " + button);

        playerButtonChoice = button;

        AIChoice(button, playerStats.influenceMultiplier);

        //aiManager.GenerateChoices(scenarios[0]);

        GenerateScenarioOutcome();
        Debug.Log("Scenario Manager: Player Button Choice: " + playerButtonChoice);
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
        Debug.Log("Scenario Manager: Finish Scenario, Button: " + button + ", Player's Button: " + playerButtonChoice);

        passedVote = button;

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
            playerStats.influence -= 5;
        }

        if(playerStats.influence < 0)
        {
            playerStats.influence = 0;
        }

        condition += button.baseCondition;

        aiManager.UpdateAiStats(button);

        scenarios.Add(scenarios[0]);
        scenarios.RemoveAt(0);

        foreach (GameObject uiButton in uIButtons)
        {
            uiButton.GetComponent<CustomButton>().scenarioButton.voteCount = 0;

            uiButton.SetActive(false);
        }

        uIButtons.Clear();

        CalculatingVotes();
    }

    void CalculatingVotes()
    {
        countingVotesUIPanel.SetActive(true);
        countingVotesUI.SetActive(true);

        float timer = 1 + Random.Range(-1f, 3f);

        Invoke("VotePassed", timer);
    }

    void VotePassed()
    {
        countingVotesUI.SetActive(false);
        votePassedUI.SetActive(true);

        votePassedText.text = "Vote Passed: " + passedVote.name;
    }

    public void NextScenario()
    {
        votePassedUI.SetActive(false);
        countingVotesUIPanel.SetActive(false);

        passedVote = null;

        UpdateUISliders();
        CheckForWin();
    }

    void CheckForWin()
    {
        if (condition >= winConditionLevel)
        {
            EndGame(true);
        }
        else if (condition <= loseConditionLevel)
        {
            EndGame(false);
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
            List<CharacterStats> aiList = new List<CharacterStats>();
            List<CharacterStats> aiInPlay = new List<CharacterStats>();
            aiList = aiManager.GetAisInPlay();
            
            foreach(CharacterStats character in aiList)
            {
                aiInPlay.Add(character);
                Debug.Log("Add Character: " + character + ", influence: " + character.influence);
            }

            float minInfluence = aiInPlay[0].influence;
            float maxInfluence = aiInPlay[aiInPlay.Count - 1].influence;
            //float n = minInfluence
            float relativeInfluence = ((playerStats.influence - minInfluence) / (maxInfluence - minInfluence));

            Debug.Log("ScenarioManager: minInfluence = " + minInfluence);
            Debug.Log("ScenarioManager: maxInfluence = " + maxInfluence);
            Debug.Log("ScenarioManager: Relative Influence = " + relativeInfluence);

            //influenceSlider.fillAmount = (playerStats.influence + 100) / 200;
            influenceSlider.fillAmount = relativeInfluence;
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

            aiManager.GenerateChoices(scenarios[0]);
        }
        else
        {
            Debug.Log("No scenarios in the list");

            EndGame(false);
        }
    }

    public void EndGame(bool win)
    {
        if (win)
        {
            SceneManager.LoadScene(winScene);
        }
        else
        {
            SceneManager.LoadScene(loseScene);
        }
    }
}
