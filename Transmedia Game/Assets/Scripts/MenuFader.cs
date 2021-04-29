using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeLength = 3;

    private void Awake()
    {
        StartFade();
    }

    public void StartFade()
    {
        StartCoroutine("FadeInCanvas");
    }

    IEnumerator FadeInCanvas()
    {
        yield return new WaitForSeconds(1);

        for (float t = 0f; t < fadeLength; t += Time.deltaTime)
        {
            canvasGroup.alpha = t;
            yield return null;
        }
    }
}
