using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button playBtn, creditsBtn, exitBtn;
    public string gameScene, creditsScene;

    public void Start()
    {
        playBtn.onClick.AddListener(PlayGame);
        creditsBtn.onClick.AddListener(ShowCredits);
        exitBtn.onClick.AddListener(ExitGame);
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
