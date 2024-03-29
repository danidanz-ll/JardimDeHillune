using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start() 
    {
        if (!PlayerPrefs.HasKey("save_level"))
        {
            PlayerPrefs.SetInt("save_level", 1);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Continue()
    {
        int level = PlayerPrefs.GetInt("save_level");
        SceneManager.LoadScene("Level_" + level);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene("Options_Menu");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
