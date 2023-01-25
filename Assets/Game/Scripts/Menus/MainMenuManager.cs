using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneLoader;
    private bool IsLoadingScene = false;
    private void Start() 
    {
        if (!PlayerPrefs.HasKey("save_level"))
        {
            PlayerPrefs.SetInt("save_level", 1);
        }
        if (sceneLoader != null)
        {
            sceneLoader.SetActive(false);
        }
    }
    public void Play()
    {
        sceneLoader.SetActive(true);
        SceneLoader.Instance.LoadSceneAsync("Level_1");
    }
    public void Continue()
    {
        sceneLoader.SetActive(true);
        int level = PlayerPrefs.GetInt("save_level");
        SceneLoader.Instance.LoadSceneAsync("Level_" + level);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        sceneLoader.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Options_Menu");
        SceneLoader.Instance.LoadSceneAsync("Options_Menu");
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
