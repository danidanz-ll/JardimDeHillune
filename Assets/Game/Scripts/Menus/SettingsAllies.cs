using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAllies : MonoBehaviour
{

    [SerializeField]
    private string AllySelected;

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
        AllySelected = "";

        List<string> items = new List<string>
        {
            "Maeve",
            "Plant Basic",
            "Obelisc"
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
        AllySelected = dropdown.options[index].text;

        if (AllySelected == "Obelisc")
        {
            GameObjectInputLife.SetActive(true);
            GameObjectInputDamage.SetActive(false);

        }
        else
        {
            GameObjectInputLife.SetActive(true);
            GameObjectInputDamage.SetActive(true);
        }

        try
        {
            InputLife.text = GetLife(AllySelected).ToString();
            InputDamage.text = GetDamage(AllySelected).ToString();
        } catch(Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
    public static int GetLife(string ally)
    {
        try
        {
            return PlayerPrefs.GetInt($"{ally}_Life");
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor de vida do {ally}.");
            Debug.LogError(ex);

            if (!PlayerPrefs.HasKey($"{ally}_Life"))
            {
                PlayerPrefs.SetInt($"{ally}_Life", 50);
            }
            return 0;
        }
    }
    public void SetLife(string ally)
    {
        try
        {
            PlayerPrefs.SetInt($"{AllySelected}_Life", Int32.Parse(ally));
        } catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor de vida do {AllySelected}.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"{AllySelected}_Life", 0);
        }
    }
    public static int GetDamage(string ally)
    {
        try
        {
            return PlayerPrefs.GetInt($"{ally}_Damage");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível obter o valor de dano do {ally}.");
            Debug.LogError(ex);

            if (!PlayerPrefs.HasKey($"{ally}_Damage"))
            {
                PlayerPrefs.SetInt($"{ally}_Damage", 10);
            }
            return 0;
        }
    }
    public void SetDamage(string ally)
    {
        try
        {
            PlayerPrefs.SetInt($"{AllySelected}_Damage", Int32.Parse(ally));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Não foi possível salvar o valor de dano do {AllySelected}.");
            Debug.LogError(ex);
            PlayerPrefs.SetInt($"{AllySelected}_Damage", 0);
        }
    }
}
