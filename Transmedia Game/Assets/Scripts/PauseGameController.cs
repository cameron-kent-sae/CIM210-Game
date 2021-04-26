using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    public Button menuBtn, continueBtn, exitBtn;
    public string sceneToLoad;
    public GameObject pauseUI;
    private bool gamePaused;

    private void Start()
    {
        gamePaused = false;

        menuBtn.onClick.AddListener(PauseGame);
        continueBtn.onClick.AddListener(ContinueGame);
        exitBtn.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (!gamePaused)
        {
            if (Input.GetKeyDown("escape"))
            {
                PauseGame();
            }
        }
        else if (gamePaused)
        {
            if (Input.GetKeyDown("escape"))
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    private void ContinueGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
