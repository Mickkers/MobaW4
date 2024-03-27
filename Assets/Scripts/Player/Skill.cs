using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [HideInInspector] public Animator animator;


    [Header("Skill Attributes")]
    public float skillCooldown;
    public float skillCost;
    public float skillDuration;

    [HideInInspector] public bool isSkillAvailable;
    [HideInInspector] public bool isSkillActive;

    public abstract void SkillAction();
    protected abstract void SkillCooldown();
    protected abstract void SkillDuration();


    public float GetManaCost()
    {
        return skillCost;
    }
}
