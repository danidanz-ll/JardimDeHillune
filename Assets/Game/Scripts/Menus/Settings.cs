using UnityEngine;

public class Settings : MonoBehaviour
{
    public static string GetResolution()
    {
        return PlayerPrefs.GetString("resolution");
    }
    public static void SetResolution(string resolution)
    {
        PlayerPrefs.SetString("resolution", resolution);
    }
    public static int GetSoundVolume()
    {
        return PlayerPrefs.GetInt("sound_volume");
    }
    public void SetSoundVolume(int volume)
    {
        PlayerPrefs.SetInt("sound_volume", volume);
    }
    public static int GetMusicVolume()
    {
        return PlayerPrefs.GetInt("music_volume");
    }
    public void SetMusicVolume(int volume)
    {
        PlayerPrefs.SetInt("music_volume", volume);
    }
    public static string GetMute()
    {
        return PlayerPrefs.GetString("mute");
    }
    public static void SetMute(string resolution)
    {
        PlayerPrefs.SetString("mute", resolution);
    }
    public static bool GetUserSettings()
    {
        if (PlayerPrefs.GetInt("UserSettings") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void SetUserSettings(bool userSettings)
    {
        if (userSettings)
        {
            PlayerPrefs.SetInt("UserSettings", 1);
        }
        else
        {
            PlayerPrefs.SetInt("UserSettings", 0);
        }
    }
}
