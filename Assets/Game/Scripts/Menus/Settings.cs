using UnityEngine;

public class Settings : MonoBehaviour
{
    public string GetResolution()
    {
        return PlayerPrefs.GetString("resolution");
    }
    public void SetResolution(string resolution)
    {
        PlayerPrefs.SetString("resolution", resolution);
    }
    public string GetSoundVolume()
    {
        return PlayerPrefs.GetString("sound_volume");
    }
    public void SetSoundVolume(string resolution)
    {
        PlayerPrefs.SetString("sound_volume", resolution);
    }
    public string GetMusicVolume()
    {
        return PlayerPrefs.GetString("music_volume");
    }
    public void SetMusicVolume(string resolution)
    {
        PlayerPrefs.SetString("music_volume", resolution);
    }
    public string GetMute()
    {
        return PlayerPrefs.GetString("mute");
    }
    public void SetMute(string resolution)
    {
        PlayerPrefs.SetString("mute", resolution);
    }
}
