using UnityEngine;
public class MenusManager : MonoBehaviour
{
    private void Start() 
    {
        if (!PlayerPrefs.HasKey("save_level"))
        {
            PlayerPrefs.SetInt("save_level", 1);
        }

        if (!PlayerPrefs.HasKey("LevelDay"))
        {
            SettingsMatch.SetLevelDay(true);
        }

        if (!PlayerPrefs.HasKey("UserSettings"))
        {
            Settings.SetUserSettings(false);
        }
    }
    public void Play()
    {
        if (SettingsMatch.GetLevelDay())
        {
            SceneLoader.Instance.LoadSceneAsync("Level_Day");
        }
        else
        {
            SceneLoader.Instance.LoadSceneAsync("Level_Night");
        }
    }
    public void Continue()
    {
        int level = PlayerPrefs.GetInt("save_level");
        //SceneLoader.Instance.LoadSceneAsync("Level_" + level);

        if (SettingsMatch.GetLevelDay())
        {
            SceneLoader.Instance.LoadSceneAsync("Level_Day");
        }
        else
        {
            SceneLoader.Instance.LoadSceneAsync("Level_Night");
        }
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
    public void CharactersSettings()
    {
        SceneLoader.Instance.LoadSceneAsync("CharactersSettings");
    }
    public void SpawnerSettings()
    {
        SceneLoader.Instance.LoadSceneAsync("SpawnerSettings");
    }
}
