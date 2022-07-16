using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float maxLife;
    [SerializeField] public float currentLife;
    [SerializeField] public Slider HealtBar;

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
        Debug.Log("Take damage!");
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
    public void UpdateHealthBar()
    {
        if (HealtBar != null)
        {
            HealtBar.value = currentLife;
        }
    }
}
