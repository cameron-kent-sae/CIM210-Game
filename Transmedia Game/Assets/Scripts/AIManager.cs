using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIManager : MonoBehaviour
{
    public ScenarioManager scenarioManager;

    public CharacterStats playerStats;
    public CharacterStats[] aiCharacters;
    private List<CharacterStats> aiCharactersInPlay;
    private List<CharacterStats> aiCharactersToSpawn;
    private List<CharacterStats> charactersInPlay;

    public int numberOfAICharacters = 6;
    private int numberOfgeneratedCharacters;
    private List<float> influenceNumber;

    private List<ScenarioButton> aiButtons;

    [Header("UI Elements")]
    public GoverningBoardManager gBManager;
    public TMP_Text[] AiTitles;
    public Image[] AiImages;

    [Header("Dialog Elements")]
    public int maxNumberOfDialogIcons = 2;
    public GameObject[] dialogeIcons;
    public GameObject dialogFrame;
    public Image dialogFrameImage;
    public TMP_Text dialogFrameTitle;
    public TMP_Text dialogFrameFlavourText;
    public TMP_Text dialogFrameVoteEffect;

    private void Start()
    {
        aiButtons = new List<ScenarioButton>();
        aiCharactersInPlay = new List<CharacterStats>();
        aiCharactersToSpawn = new List<CharacterStats>();
        influenceNumber = new List<float>();

        if (!scenarioManager)
        {
            scenarioManager = GameObject.Find("ScenarioManager").GetComponent<ScenarioManager>();
        }

        GenerateCharacters();
    }

    void GenerateCharacters()
    {
        foreach(CharacterStats ai in aiCharacters)
        {
            aiCharactersToSpawn.Add(ai);
        }

        while (numberOfgeneratedCharacters < numberOfAICharacters)
        {
            int rand = Random.Range(0, aiCharactersToSpawn.Count);

            //Debug.Log("AI Manager: random number: " + rand);

            if (aiCharactersInPlay.Count == 0)
            {
                AddAICharacter(aiCharacters[rand]);
            }
            else
            {
                AddAICharacter(aiCharactersToSpawn[rand]);
            }
        }

        //AddAICharacter(playerStats);
        SortCharacters();
    }

    void AddAICharacter(CharacterStats character)
    {
        aiCharactersInPlay.Add(character);
        aiCharactersToSpawn.Remove(character);

        character.influence = character.startingInfluence;

        //Debug.Log("AI Manager: add character: " + character);

        numberOfgeneratedCharacters++;
    }

    public void UpdateAiStats(ScenarioButton button)
    {
        for (int i = 0; i < aiCharactersInPlay.Count; i++)
        {
            if (aiButtons[i] == button)
            {
                aiCharactersInPlay[i].influence += button.baseInfluence;
            }
            else
            {
                aiCharactersInPlay[i].influence -= 5;
            }

            if (aiCharactersInPlay[i].influence < 0)
            {
                aiCharactersInPlay[i].influence = 0;
            }
        }

        SortCharacters();
    }

    void SortCharacters()
    {
        Debug.Log("AI Manager: Sort characters");

        charactersInPlay = aiCharactersInPlay;
        charactersInPlay.Add(playerStats);

        charactersInPlay.Sort(SortByInfluence);

        gBManager.characters = aiCharactersInPlay;

        // Set influence multipliers
        charactersInPlay[charactersInPlay.Count - 1].influenceMultiplier = 3;
        charactersInPlay[charactersInPlay.Count - 2].influenceMultiplier = 2;
        charactersInPlay[charactersInPlay.Count - 3].influenceMultiplier = 2;

        foreach(CharacterStats ai in aiCharactersInPlay)
        {
            for(int i = charactersInPlay.Count - 4; i >= 0; i--)
            {
                charactersInPlay[i].influenceMultiplier = 1;
            }
        }

        UpdateAIUI();
    }

    void UpdateAIUI()
    {
        charactersInPlay.Remove(playerStats);

        for (int i = 0; i < numberOfAICharacters; i++)
        {
            AiTitles[i].text = charactersInPlay[i].characterName;
            AiImages[i].sprite = charactersInPlay[i].characterSprite;
        }
    }

    int SortByInfluence(CharacterStats ai1, CharacterStats ai2)
    {
        return ai1.influence.CompareTo(ai2.influence);
    }

    public void GenerateChoices(Scenario scenario)
    {
        Debug.Log("AI Manager: Generate Choices");

        aiButtons.Clear();

        foreach (CharacterStats ai in aiCharactersInPlay)
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
                    ai.buttonChoice = scenario.scenarioButtons[i];
                }

                counter += buttonChance[i];

                //Debug.Log("AI Manager: ai: " + ai + ", total chance weighting: " + totalChanceWeighting + ", i: " + i + " / " + buttonChance.Count);
            }
        }

        UpdateDialogeIcon();
        //scenarioManager.GenerateScenarioOutcome();
    }

    void UpdateDialogeIcon()
    {
        /*
        for(int i = 0; i < gBManager.characters.Count; i++)
        {
            if(gBManager.characters[i].characterName != playerStats.characterName)
            {
                dialogeIcons[i].SetActive(true);
            }
            else
            {
                dialogeIcons[i].SetActive(false);
            }
        }
        */
        List<GameObject> dialogsToLoad = new List<GameObject>();

        dialogsToLoad.AddRange(dialogeIcons);

        int randomNumber = Random.Range(1, maxNumberOfDialogIcons + 1);

        for (int i = 0; i < randomNumber; i++)
        {
            int rand = Random.Range(0, dialogsToLoad.Count);

            dialogeIcons[rand].GetComponent<Image>().enabled = true;

            dialogsToLoad.RemoveAt(rand);
        }

        foreach(GameObject icon in dialogsToLoad)
        {
            icon.GetComponent<Image>().enabled = false;
        }
    }

    public void ResetDialogeUI()
    {
        dialogFrame.SetActive(false);
    }

    public void OpenDialogUI(int i)
    {
        dialogFrame.SetActive(true);
        dialogFrameImage.sprite = gBManager.characters[i].characterSprite;
        dialogFrameTitle.text = gBManager.characters[i].characterName;
        dialogFrameFlavourText.text = "I will be voting " + gBManager.characters[i].buttonChoice.name;

        dialogFrameVoteEffect.text = gBManager.characters[i].buttonChoice.name + " will effect: INF " + gBManager.characters[i].buttonChoice.baseInfluence + " / CON " + gBManager.characters[i].buttonChoice.baseCondition;
    }

    public void CloseDialogUI()
    {
        dialogFrame.SetActive(false);
    }

    public List<CharacterStats> GetAisInPlay()
    {
        return aiCharactersInPlay;
    }
}
