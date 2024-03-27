using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float damageMultiplier;
    public float DamageMultiplier
    {
        get => damageMultiplier;
        private set
        {
            damageMultiplier = value;
        }
    }

    [Header("Stats Increases")]
    [SerializeField] private float healthIncrease;
    [SerializeField] private float manaIncrease;
    [SerializeField] private float damageIncrease;

    public static PlayerController PlayerOne { get; private set; }
    public static PlayerController PlayerTwo { get; private set; }

    [SerializeField] private bool isPlayerOne;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float meleeMaxHealth;
    [SerializeField] private float rangedMaxHealth;

    [Header("Melee")]
    [SerializeField] private GameObject meleeChar;
    [SerializeField] private MutantAction meleeAction;
    [SerializeField] private MutantSkill meleeSkill;
    [SerializeField] private MutantUltimate meleeUltimate;

    [Header("Ranged")]
    [SerializeField] private GameObject rangeChar;
    [SerializeField] private MjlAction rangeAction;
    [SerializeField] private MjlSkill rangeSkill;
    [SerializeField] private MjlUltimate rangeUltimate;

    public PlayerAction PAction { get; private set; }

    public EnumCharType CharType { get; private set; }

    private bool isMelee;

    private void Awake()
    {
        if (isPlayerOne)
        {
            PlayerOne = this;
        }
        else
        {
            PlayerTwo = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (isPlayerOne)
        {
            CharType = CharSelect.Instance.pOneSelection;
        }
        else
        {
            CharType = CharSelect.Instance.pTwoSelection;
        }

        if (CharType == EnumCharType.Melee)
        {
            playerHealth.MaxHealth = meleeMaxHealth;
            isMelee = true;
            PAction = meleeAction;
            meleeChar.SetActive(true);
            rangeChar.SetActive(false);
        }
        else
        {
            playerHealth.MaxHealth = rangedMaxHealth;
            PAction = rangeAction;
            isMelee = false;
            meleeChar.SetActive(false);
            rangeChar.SetActive(true);
        }

        Invoke(nameof(DisableOutlines), 0.01f);
    }

    private void DisableOutlines()
    {
        meleeChar.GetComponent<Outline>().enabled = false;
        rangeAction.GetComponent<Outline>().enabled = false;
    }
    public void LevelUp()
    {
        playerHealth.MaxHealth += healthIncrease;
        PAction.MaxMana += manaIncrease;
        DamageMultiplier += damageIncrease;
    }

    public void FullHealMana()
    {
        playerHealth.HealToFull();
        PAction.ManaToFull();
    }

    #region Child Components

    public Outline GetOutline()
    {
        if (isMelee)
        {
            return meleeChar.GetComponent<Outline>();
        }
        else
        {
            return rangeChar.GetComponent<Outline>();
        }
    }

    #endregion

    #region Inputs

    public void Move(Vector2 input)
    {
        playerMovement.Move(input);
    }

    public void Basic()
    {
        playerMovement.MoveToTarget();

        if (!isMelee) rangeSkill.SkillAction();
    }
    public void Skill()
    {
        PAction.Skill();
    }
    public void Ultimate()
    {
        PAction.Ultimate();
    }
    #endregion

    #region Stats
    public float GetAttackRange()
    {
        if (isMelee)
        {
            return meleeAction.GetAttackRange();
        }
        else
        {
            return rangeAction.GetAttackRange();
        }
    }

    #endregion
}
