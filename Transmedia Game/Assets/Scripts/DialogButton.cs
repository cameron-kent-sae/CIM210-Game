using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButton : MonoBehaviour
{
    public int dialogeButtonNumber;

    private AIManager aIManager;

    private void Start()
    {
        aIManager = GameObject.Find("AI Manager").GetComponent<AIManager>();
    }

    public void ButtonSelected()
    {
        Debug.Log("Press Dialog button");
        aIManager.OpenDialogUI(dialogeButtonNumber);
    }
}
