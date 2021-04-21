using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
    public Button startBtn;
    public string sceneToLoad;

    private void Start()
    {
        startBtn.onClick.AddListener(BeginGame);
    }

    private void BeginGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
