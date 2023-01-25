using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ManaEvents))]
public class ManaSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float maxMana;
    [SerializeField] public float currentMana;
    [SerializeField] private Slider ManaBar;
    [SerializeField][Min(0)] private float ManaPointRate = 0;

    private ManaEvents manaEvents;

    private void Start()
    {
        manaEvents = GetComponent<ManaEvents>();
        currentMana = maxMana;
        if (ManaBar != null)
        {
            ManaBar.maxValue = maxMana;
            ManaBar.value = currentMana;
        }

        if (manaEvents != null)
        {
            manaEvents.GetManaPoints += GetManaPoints;
            manaEvents.UseManaEvent += UseManaEvent;
        }
    }
    private void OnDestroy()
    {
        if (manaEvents != null)
        {
            manaEvents.GetManaPoints -= GetManaPoints;
            manaEvents.UseManaEvent -= UseManaEvent;
        }
    }
    private void GetManaPoints()
    {
        RegenerateMana(ManaPointRate);
    }
    public void RegenerateMana(float points)
    {
        if (currentMana + points <= maxMana)
        {
            currentMana += points;
            UpdateManaBar();
        }
    }
    public void SetFullMana()
    {
        currentMana = maxMana;
        UpdateManaBar();
    }
    public void UseManaEvent(object sender, float points)
    {
        UseMana(points);
    }
    public void UseMana(float points)
    {
        currentMana -= points;
        UpdateManaBar();
        if (currentMana <= 0)
        {

        }
    }
    public void UpdateManaBar()
    {
        if (ManaBar != null)
        {
            ManaBar.value = currentMana;
        }
    }
}
