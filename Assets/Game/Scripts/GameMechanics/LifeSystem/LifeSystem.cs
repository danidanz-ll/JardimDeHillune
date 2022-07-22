using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float maxLife;
    [SerializeField] public float currentLife;
    [SerializeField] private Slider HealtBar;
    [SerializeField] private float InvencibleTimeDamage;
    [SerializeField] private float TimeDamage;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    public bool IsHurting { get; private set; } = false;
    public bool IsInvencible { get; private set; } = false;

    private void Start()
    {
        currentLife = maxLife;
        if (HealtBar != null)
        {
            HealtBar.maxValue = maxLife;
            HealtBar.value = currentLife;
        }
    }
    public bool TakeDamage(float damage)
    {
        if (!IsInvencible)
        {
            StartCoroutine(PlayHurtingAnimation());
            StartCoroutine(TimeInvencible());
        
            currentLife -= damage;
            UpdateHealthBar();
            if (currentLife <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    public void UpdateHealthBar()
    {
        if (HealtBar != null)
        {
            HealtBar.value = currentLife;
        }
    }
    
    public IEnumerator PlayHurtingAnimation()
    {
        IsHurting = true;
        yield return new WaitForSeconds(TimeDamage);
        IsHurting = false;
    }
    public IEnumerator TimeInvencible()
    {
        IsInvencible = true;
        yield return new WaitForSeconds(InvencibleTimeDamage);
        IsInvencible = false;
    }
}
