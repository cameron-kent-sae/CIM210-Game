using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button playBtn, creditsBtn, exitBtn;
    public string gameScene, creditsScene;
    public CanvasGroup canvasGroup;
    public float fadeLength;

    public void Start()
    {
        playBtn.onClick.AddListener(PlayGame);
        creditsBtn.onClick.AddListener(ShowCredits);
        exitBtn.onClick.AddListener(ExitGame);

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

    private void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    private void ShowCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
