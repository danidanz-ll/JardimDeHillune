using System;
using UnityEngine;

public class ManaEvents : MonoBehaviour
{
    public event Action GetManaPoints;
    public event EventHandler<float> UseManaEvent; 

    private void Start()
    {
    }
    private void OnDestroy()
    {
    }
    public void GetMana()
    {
        GetManaPoints.Invoke();
    }
    public void UseMana(float points)
    {
        UseManaEvent.Invoke(this, points);
    }
}
