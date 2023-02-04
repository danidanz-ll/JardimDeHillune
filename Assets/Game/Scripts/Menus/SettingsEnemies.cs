using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsEnemies : MonoBehaviour
{
    [SerializeField]
    private bool IsMenu = true;

    [SerializeField]
    private string EnemySelected;

    [SerializeField]
    private TMPro.TMP_InputField InputLife;

    [SerializeField]
    private TMPro.TMP_InputField InputDamage;

    [SerializeField]
    private GameObject GameObjectInputLife;

    [SerializeField]
    private GameObject GameObjectInputDamage;

    private TMPro.TMP_Dropdown dropdown;
    private void Start()
    {
        dropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();
        dropdown.options.Clear();
        EnemySelected = "";

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
        GameObjectInputLife.SetActive(false);
        GameObjectInputDamage.SetActive(false);

    }
    private void DropdownItemSelected(TMPro.TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        EnemySelected = dropdown.options[index].text;

        GameObjectInputLife.SetActive(true);
        GameObjectInputDamage.SetActive(true);

        try
        {
            InputLife.text = GetLife(EnemySelected).ToString();
            InputDamage.text = GetDamage(EnemySelected).ToString();
        } catch(Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
    public static int GetLife(string enemy)
    {
        try
        {
            return PlayerPrefs.GetInt($"{enemy}_Life");
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor de vida do {enemy}.");
            Debug.LogError(ex);

            if (!PlayerPrefs.HasKey($"{enemy}_Life"))
            {
                PlayerPrefs.SetInt($"{enemy}_Life", 50);
            }
            return 0;
        }
    }
    public void SetLife(string enemy)
    {
        try
        {
            PlayerPrefs.SetInt($"{EnemySelected}_Life", Int32.Parse(enemy));
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor de vida do {EnemySelected}.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"{EnemySelected}_Life", 0);
        }
    }
    public static int GetDamage(string enemy)
    {
        try
        {
            return PlayerPrefs.GetInt($"{enemy}_Damage");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor de dano do {enemy}.");
            Debug.LogError(ex);

            if (!PlayerPrefs.HasKey($"{enemy}_Damage"))
            {
                PlayerPrefs.SetInt($"{enemy}_Damage", 10);
            }
            return 0;
        }
    }
    public void SetDamage(string enemy)
    {
        try
        {
            PlayerPrefs.SetInt($"{EnemySelected}_Damage", Int32.Parse(enemy));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor de dano do {EnemySelected}.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"{EnemySelected}_Damage", 0);
        }
    }
}
