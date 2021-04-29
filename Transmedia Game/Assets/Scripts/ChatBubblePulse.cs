using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubblePulse : MonoBehaviour
{
    private float approachSpeed = 0.8f;
    private float growthBound = 1.1f;
    private float shrinkBound = 0.8f;
    private float currentRatio = 1;
    private Image icon;
    private Coroutine routine;
    public bool keepGoing = true;

    void Awake()
    {
        icon = gameObject.GetComponent<Image>();
        StartCoroutine(Pulse());

        //StartPulser();
    }

    public void StartPulser()
    {
        routine = StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed * Time.deltaTime);

                // Update our text element
                this.icon.transform.localScale = Vector3.one * currentRatio;
                //this.icon.text = "Growing!";

                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (this.currentRatio != this.shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed * Time.deltaTime);

                // Update our text element
                this.icon.transform.localScale = Vector3.one * currentRatio;
                //this.icon.text = "Shrinking!";

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
