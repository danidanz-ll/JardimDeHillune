using UnityEngine;
public class MenusManager : MonoBehaviour
{
    private bool IsLoadingScene = false;
    private void Start() 
    {
        if (!PlayerPrefs.HasKey("save_level"))
        {
            PlayerPrefs.SetInt("save_level", 1);
        }
    }
    public void Play()
    {
        SceneLoader.Instance.LoadSceneAsync("Level_1");
    }
    public void Continue()
    {
        int level = PlayerPrefs.GetInt("save_level");
        SceneLoader.Instance.LoadSceneAsync("Level_" + level);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneLoader.Instance.LoadSceneAsync("Options_Menu");
    }

    public void Menu()
    {
        SceneLoader.Instance.LoadSceneAsync("Menu");
    }
}
