using System;
using System.Collections;
using UnityEngine;

public class TakeOnDamage : MonoBehaviour, IDamageable
{
    [SerializeField][Range(0.0f, 100.0f)] public float AttackEscape;
    public event Action DamageEvent;
    public event EventHandler<float> DamageValueEvent; 
    public bool IsHurting { get; private set; } = false;
    public void TakeDamage(float damage)
    {
        if (UnityEngine.Random.Range(0, 100) >= AttackEscape)
        {
            IsHurting = true;
            DamageEvent.Invoke();
            DamageValueEvent.Invoke(this, damage);
        }
    }
}
