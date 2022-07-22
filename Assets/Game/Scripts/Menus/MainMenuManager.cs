using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string scenePlay;
    public void Play()
    {
        SceneManager.LoadScene(scenePlay);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
