using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMatch : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_InputField MatchTimeValue;

    [SerializeField]
    private Toggle MatchDay;

    [SerializeField]
    private Toggle UserSettings;

    private void Start()
    {
        if (MatchTimeValue != null)
        {
            MatchTimeValue.text = GetTimeMatch().ToString();
        }

        if (MatchDay != null)
        {
            MatchDay.isOn = GetLevelDay();
        }

        if (UserSettings != null)
        {
            UserSettings.isOn = Settings.GetUserSettings();
        }
    }
    public static float GetTimeMatch()
    {
        try
        {
            return PlayerPrefs.GetFloat($"TimeMatch");
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor de TimeMatch.");
            Debug.LogError(ex);
            return 0;
        }
    }
    public void SetTimeMatch(string time)
    {
        try
        {
            PlayerPrefs.SetFloat($"TimeMatch", Int32.Parse(time));
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor de TimeMatch.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"TimeMatch", 0);
        }
    }
    public static bool GetLevelDay()
    {
        try
        {
            if (PlayerPrefs.GetInt($"LevelDay") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor de LevelDay.");
            Debug.LogError(ex);
            return false;
        }
    }
    public static void SetLevelDay(bool day)
    {
        try
        {
            if (day)
            {
                PlayerPrefs.SetInt($"LevelDay", 1);
            }
            else
            {
                PlayerPrefs.SetInt($"LevelDay", 0);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor de LevelDay.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"LevelDay", 1);
        }
    }
}
