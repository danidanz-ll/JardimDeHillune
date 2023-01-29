using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSpawners : MonoBehaviour
{
    [SerializeField]
    private bool IsMenu = true;

    [SerializeField]
    private string SpawnerSelected;

    [SerializeField]
    private TMPro.TMP_InputField InputMaxMobs;

    [SerializeField]
    private TMPro.TMP_InputField InputSpawnInterval;

    [SerializeField]
    private GameObject GameObjectInputMaxMobs;

    [SerializeField]
    private GameObject GameObjectInputSpawnInterval;

    private TMPro.TMP_Dropdown dropdown;
    private void Start()
    {
        dropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();
        dropdown.options.Clear();
        SpawnerSelected = "";

        List<string> items = new List<string>
        {
            "Skeleton",
            "Goblin",
            "Mushroom",
            "Eye"
        };

        foreach (string item in items)
        {
            dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = item });
        }
        
        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        GameObjectInputMaxMobs.SetActive(false);
        GameObjectInputSpawnInterval.SetActive(false);

    }
    private void DropdownItemSelected(TMPro.TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        SpawnerSelected = dropdown.options[index].text;

        GameObjectInputMaxMobs.SetActive(true);
        GameObjectInputSpawnInterval.SetActive(true);

        try
        {
            InputMaxMobs.text = GetMaxMob(SpawnerSelected).ToString();
            InputSpawnInterval.text = GetSpawnInterval(SpawnerSelected).ToString();
        } catch(Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
    public static int GetMaxMob(string spawner)
    {
        try
        {
            return PlayerPrefs.GetInt($"{spawner}_MaxMob");
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor máximo de mobs do spawner {spawner}.");
            Debug.LogError(ex);
            return 0;
        }
    }
    public void SetMaxMob(string maxMob)
    {
        try
        {
            PlayerPrefs.SetInt($"{SpawnerSelected}_MaxMob", Int32.Parse(maxMob));
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor máximo de mobs do spawner {SpawnerSelected}.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"{SpawnerSelected}_MaxMob", 0);
        }
    }
    public static int GetSpawnInterval(string spawner)
    {
        try
        {
            return PlayerPrefs.GetInt($"{spawner}_SpawnInterval");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor de intervalo de spawn do spawner {spawner}.");
            Debug.LogError(ex);
            return 0;
        }
    }
    public void SetSpawnInterval(string spawnInterval)
    {
        try
        {
            PlayerPrefs.SetInt($"{SpawnerSelected}_SpawnInterval", Int32.Parse(spawnInterval));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor de intervalo de spawn do spawner {SpawnerSelected}.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"{SpawnerSelected}_SpawnInterval", 0);
        }
    }
}
