using System.Collections;
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
            //foreach
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
