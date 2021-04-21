using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    public Button continueBtn, exitBtn;
    public string sceneToLoad;
    public GameObject pauseUI;

    private void Start()
    {
        continueBtn.onClick.AddListener(ContinueGame);
        exitBtn.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
