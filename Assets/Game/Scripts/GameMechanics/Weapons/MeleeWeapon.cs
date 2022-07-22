using System.Collections;
using UnityEngine;

public class MeleeWeapon : TriggerDamageConstant, IWeapon
{
    [SerializeField] 
    private float attackTime = 0.2f;
    [SerializeField]
    private float timeToFreeze = 0.2f;

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
        //Debug.Log("Attacking weapon!");
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        Attacking = true;
        yield return new WaitForSeconds(attackTime);
        Attacking = false;
        gameObject.SetActive(false);
        yield break;
    }

    public bool IsAttacking()
    {
        return Attacking;
    }

    public float GetAttackingTime()
    {
        return attackTime;
    }

    public float GetWaitToFreezeTime()
    {
        return timeToFreeze;
    }
}
