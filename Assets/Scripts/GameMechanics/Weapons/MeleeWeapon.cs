using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : TriggerDamageConstant, IWeapon
{
    [SerializeField] 
    private float attackTime = 0.2f;

    private bool Attacking = false;
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    private void Start()
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
        Attacking = true;
        yield return new WaitForSeconds(attackTime);
        gameObject.SetActive(false);
        Attacking = false;
    }

    public bool IsAttacking()
    {
        return Attacking;
    }
}
