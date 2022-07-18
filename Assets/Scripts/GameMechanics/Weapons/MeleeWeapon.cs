using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : TriggerDamagePlayer, IWeapon
{
    [SerializeField] 
    private float attackTime = 0.2f;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void Attack()
    {
        gameObject.SetActive(true);
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(attackTime);
        gameObject.SetActive(false);
    }
}
