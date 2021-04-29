using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button playBtn, creditsBtn, exitBtn;
    public string gameScene, creditsScene;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        playBtn.onClick.AddListener(PlayGame);
        creditsBtn.onClick.AddListener(ShowCredits);
        exitBtn.onClick.AddListener(ExitGame);

    }

    private void PlayGame()
    {
        audioSource.Play();
        SceneManager.LoadScene(gameScene);
    }
    private void ShowCredits()
    {
        audioSource.Play();
        SceneManager.LoadScene(creditsScene);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
