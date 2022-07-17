using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : TriggerDamage, IWeapon
{
    [SerializeField] private float attackTime = 0.2f;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void Attack()
    {
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        gameObject.SetActive(false);
    }
}
