using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float maxLife;
    [SerializeField] public float currentLife;
    [SerializeField] public Slider HealtBar;
    [SerializeField] public float InvencibleTimeDamage;
    [SerializeField] public float TimeDamage;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private bool isInvencible = false;

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
        if (!isInvencible)
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
        animator.SetBool("isHurting", true);
        yield return new WaitForSeconds(TimeDamage);
        animator.SetBool("isHurting", false);
    }
    public IEnumerator TimeInvencible()
    {
        isInvencible = true;
        yield return new WaitForSeconds(InvencibleTimeDamage);
        isInvencible = false;
    }
}
