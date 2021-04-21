using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public ScenarioManager scenarioManager;

    public AIStats[] aiCharacters;
    private List<AIStats> aiCharactersInPlay;

    public int numberOfAICharacters = 6;

    private List<ScenarioButton> aiButtons;

    private void Start()
    {
        aiButtons = new List<ScenarioButton>();

        if (!scenarioManager)
        {
            scenarioManager = GameObject.Find("ScenarioManager").GetComponent<ScenarioManager>();
        }

        GenerateCharacters();
    }

    void GenerateCharacters()
    {
        List<AIStats> pickedCharacters = new List<AIStats>();

        int numberOfgeneratedCharacters = 0;

        while(numberOfAICharacters < numberOfAICharacters + 1)
        {
            int rand = Random.Range(0, aiCharacters.Length);

            if (pickedCharacters.Count == 0)
            {
                pickedCharacters.Add(aiCharacters[rand]);

                numberOfgeneratedCharacters++;
            }
            else
            {
                foreach(AIStats pickedChar in pickedCharacters)
                {
                    if(pickedChar != aiCharacters[rand])
                    {
                        pickedCharacters.Add(aiCharacters[rand]);

                        numberOfgeneratedCharacters++;
                    }
                }
            }
        }

        SortCharacters();
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
        List<float> influenceNumber = new List<float>();

        foreach (AIStats ai in aiCharactersInPlay)
        {
            influenceNumber.Add(ai.influence);
        }

        influenceNumber.Sort();

        for(int a = 0; a < aiCharactersInPlay.Count; a++)
        {
            for (int i = 0; i < influenceNumber.Count; i++)
            {
                if (aiCharactersInPlay[a].influence == influenceNumber[i])
                {
                    //aiCharacters.
                }
            }
        }
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
                    float chance = ai.influence;
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
