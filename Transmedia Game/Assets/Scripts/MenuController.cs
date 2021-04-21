using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button playBtn, exitBtn;
    public string sceneToLoad;

    public void Start()
    {
        playBtn.onClick.AddListener(PlayGame);
        exitBtn.onClick.AddListener(ExitGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
