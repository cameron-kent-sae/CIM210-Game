﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public AIStats[] aiCharacters;

    public void UpdateAiStats(float influence, float condition)
    {

    }

    public void GenerateChoices(Scenario scenario)
    {
        foreach(AIStats ai in aiCharacters)
        {
            List<float> buttonChance = new List<float>();
            List<ScenarioButton> priorityButtons = new List<ScenarioButton>();

            int numbOfPriorityoptions = 0;

            foreach (ScenarioButton button in scenario.scenarioButtons)
            {
                float additionalButtonbias = 0;

                if(button.baseInfluence >= ai.maxInfluenceThreshold || button.baseCondition >= ai.maxConditionThreshold)
                {
                    numbOfPriorityoptions++;

                    priorityButtons.Add(button);
                }
                else
                {
                    if(button.baseInfluence > 0 && button.baseCondition > 0)
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
                }

                buttonChance.Add(100 + additionalButtonbias);
            }

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
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
