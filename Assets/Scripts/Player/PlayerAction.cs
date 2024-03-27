using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class PlayerAction : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;

    [Header("Mana Attributes")]
    public float currMana;
    [SerializeField] private float maxMana;
    public float MaxMana
    {
        get => maxMana;
        set
        {
            maxMana = value;
            ManaToFull();
        }
    }
    [SerializeField] private float manaRegen;
    [Header("Basic Attack Attributes")]
    [SerializeField] private float damageMin;
    [SerializeField] private float damageMax;
    [SerializeField] private float attackRange;
    public float attackCooldown;
    [HideInInspector] public bool canBasicAttack;

    [SerializeField] protected Animator animator;
    [SerializeField] protected PlayerMovement playerMovement;
    public GameObject Target { get; protected set; }
    protected float nextAttackTime;
    protected float attackInterval;

    public void Initialization()
    {
        canBasicAttack = true;
        animator = GetComponent<Animator>();
        currMana = MaxMana;

        attackInterval = attackCooldown / (240 + attackCooldown) * 0.01f;
    }

    public bool HasMana(float value)
    {
        return value <= currMana;
    }

    public void UpdateMana()
    {
        if (currMana < MaxMana) currMana += manaRegen * Time.deltaTime;
        currMana = Mathf.Clamp(currMana, 0, MaxMana);
        playerUI.UpdateManaUI(currMana, MaxMana);
    }

    public void BasicAttack()
    {
        Target = playerMovement.target;

        if (Target != null && canBasicAttack)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) <= GetAttackRange() && Time.time > nextAttackTime)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        canBasicAttack = false;
        animator.SetBool(AnimationStrings.basicAttack, true);

        yield return new WaitForSeconds(attackInterval);

        if (Target == null || Target.CompareTag("DeadEnemy") || Target.CompareTag("DeadPlayer"))
        {
            canBasicAttack = true;
            animator.SetBool(AnimationStrings.basicAttack, false);
        }
    }
    public abstract void Skill();
    public abstract void Ultimate();

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetDamage()
    {
        return Mathf.Lerp(damageMin, damageMax, Random.Range(0f, 1f)) * GetComponentInParent<PlayerController>().DamageMultiplier;
    }

    public void ManaToFull()
    {
        currMana = MaxMana;
    }
}
