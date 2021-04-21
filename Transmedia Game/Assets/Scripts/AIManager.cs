﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public ScenarioManager scenarioManager;

    public AIStats[] aiCharacters;
    private List<AIStats> aiCharactersInPlay;
    private List<AIStats> pickedCharacters;

    public int numberOfAICharacters = 6;
    private int numberOfgeneratedCharacters;
    private List<float> influenceNumber;

    private List<ScenarioButton> aiButtons;

    private void Start()
    {
        aiButtons = new List<ScenarioButton>();
        aiCharactersInPlay = new List<AIStats>();
        pickedCharacters = new List<AIStats>();
        influenceNumber = new List<float>();

        if (!scenarioManager)
        {
            scenarioManager = GameObject.Find("ScenarioManager").GetComponent<ScenarioManager>();
        }

        GenerateCharacters();
    }

    void GenerateCharacters()
    {
        while (numberOfgeneratedCharacters < numberOfAICharacters)
        {
            int rand = Random.Range(0, aiCharacters.Length);

            Debug.Log("AI Manager: random number: " + rand);

            if (pickedCharacters.Count == 0)
            {
                AddAICharacter(aiCharacters[rand]);
            }
            else
            {
                pickedCharacters = aiCharactersInPlay;

                foreach (AIStats pickedChar in pickedCharacters)
                {
                    if (pickedChar != aiCharacters[rand])
                    {
                        AddAICharacter(aiCharacters[rand]);
                    }
                }
            }
        }

        SortCharacters();
    }

    void AddAICharacter(AIStats character)
    {
        //pickedCharacters.Add(character);
        aiCharactersInPlay.Add(character);

        Debug.Log("AI Manager: add character: " + character);

        numberOfgeneratedCharacters++;
    }

    public void UpdateAiStats(ScenarioButton button)
    {
        for(int i = 0; i < aiCharactersInPlay.Count; i++)
        {
            if(aiButtons[i] == button)
            {
                aiCharactersInPlay[i].influence += button.baseInfluence;
            }
            else
            {
                aiCharactersInPlay[i].influence -= 15;
            }
        }
    }

    void SortCharacters()
    {
        /*
        List<AIStats> aiInPlay = new List<AIStats>();

        aiInPlay = aiCharactersInPlay;
        //aiCharactersInPlay.Clear();
        //aiCharactersInPlay = new List<AIStats>(aiInPlay.Count);

        foreach (AIStats ai in aiInPlay)
        {
            influenceNumber.Add(ai.influence);
        }

        influenceNumber.Sort();

        for(int a = 0; a < aiInPlay.Count; a++)
        {
            for (int i = 0; i < influenceNumber.Count; i++)
            {
                if (aiInPlay[a].influence == influenceNumber[i])
                {
                    aiCharactersInPlay.Insert(i, aiCharactersInPlay[a]);
                }
            }
        }
        */

        aiCharactersInPlay.Sort(SortByInfluence);

        // Set influence multipliers
        aiCharactersInPlay[0].influenceMultiplier = 3;
        aiCharactersInPlay[1].influenceMultiplier = 2;
        aiCharactersInPlay[2].influenceMultiplier = 2;

        foreach(AIStats ai in aiCharactersInPlay)
        {
            for(int i = 3; i < aiCharactersInPlay.Count; i++)
            {
                aiCharacters[i].influenceMultiplier = 1;
            }
        }
    }

    int SortByInfluence(AIStats ai1, AIStats ai2)
    {
        return ai1.influence.CompareTo(ai2.influence);
    }

    public void GenerateChoices(Scenario scenario)
    {
        Debug.Log("AI Manager: Generate Choices");

        aiButtons.Clear();

        foreach(AIStats ai in aiCharactersInPlay)
        {
            List<float> buttonChance = new List<float>();

            foreach (ScenarioButton button in scenario.scenarioButtons)
            {
                float additionalButtonbias = 0;

                if (button.baseInfluence > 0 && button.baseCondition > 0)
                {
                    additionalButtonbias += ai.influenceUpConditionUp;
                }
                else if (button.baseInfluence <= 0 && button.baseCondition <= 0)
                {
                    additionalButtonbias += ai.influenceDownConditionUDown;
                }
                else if (button.baseInfluence > 0 && button.baseCondition <= 0)
                {
                    additionalButtonbias += ai.influenceUpConditionDown;
                }
                else if (button.baseInfluence <= 0 && button.baseCondition > 0)
                {
                    additionalButtonbias += ai.influenceDownConditionUp;
                }

                buttonChance.Add(100 + additionalButtonbias);
            }

            float totalChanceWeighting = 0;

            foreach (float chance in buttonChance)
            {
                totalChanceWeighting += chance;
            }

            float randomNumber = Random.Range(1, totalChanceWeighting);
            float counter = 0;

            for (int i = 0; i < buttonChance.Count; i++)
            {
                if (randomNumber > counter && randomNumber < counter + buttonChance[i])
                {
                    int chance = ai.influenceMultiplier;
                    scenarioManager.AIChoice(scenario.scenarioButtons[i], chance);

                    aiButtons.Add(scenario.scenarioButtons[i]);
                }

                counter += buttonChance[i];

                Debug.Log("AI Manager: ai: " + ai + ", total chance weighting: " + totalChanceWeighting + ", i: " + i + " / " + buttonChance.Count);
            }

            /*
            if(numbOfPriorityoptions == 1)
            {
                // Play priorityButtons[i];
            }
            else if (numbOfPriorityoptions > 1)
            {
                List<float> priorityValue = new List<float>();

                foreach(ScenarioButton priority in priorityButtons)
                {
                    if(priority.baseInfluence >= ai.maxInfluenceThreshold || priority.baseCondition >= ai.maxConditionThreshold)
                    {
                        float winningDifference = priority.baseInfluence - priority.baseCondition;

                        // If positive or the same then influecen will win
                        if (winningDifference >= 0)
                        {
                            float influenceDifference = 0;
                            influenceDifference = ai.maxInfluenceThreshold - priority.baseInfluence;

                            priorityValue.Add(Mathf.Abs(influenceDifference));
                        }
                        else
                        {
                            float conditioninfluence = 0;
                            conditioninfluence = ai.maxConditionThreshold - priority.baseCondition;

                            priorityValue.Add(Mathf.Abs(conditioninfluence));
                        }
                    }
                    else if (priority.baseInfluence >= ai.maxInfluenceThreshold)
                    {
                        float influenceDifference = 0;
                        influenceDifference = ai.maxInfluenceThreshold - priority.baseInfluence;

                        priorityValue.Add(Mathf.Abs(influenceDifference));
                    }
                    else
                    {
                        float conditioninfluence = 0;
                        conditioninfluence = ai.maxConditionThreshold - priority.baseCondition;

                        priorityValue.Add(Mathf.Abs(conditioninfluence));
                    }
                }

                // Figure out which is the highest value in the priority Value, then play the priority option with the highest value
            }
            else
            {
                float totalSoundWeighting = 0;

                foreach (float chance in buttonChance)
                {
                    totalSoundWeighting += chance;
                }

                float randomNumber = Random.Range(1, totalSoundWeighting);
                float counter = 0;

                for (int i = 0; i < buttonChance.Count; i++)
                {
                    if (randomNumber > counter && randomNumber < counter + buttonChance[i])
                    {
                        // Play button
                    }

                    counter += buttonChance[i];
                }
            }
            */
        }

        scenarioManager.GenerateScenarioOutcome();
    }
}
