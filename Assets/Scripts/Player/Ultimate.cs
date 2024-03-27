using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ultimate : MonoBehaviour
{
    [HideInInspector] public Animator animator;


    [Header("Ultimate Attributes")]
    public float ultimateCooldown;
    public float ultimateCost;
    public float ultimateDamage;
    public float ultimateRange;
    public float ultimateDuration;

    [HideInInspector] public bool isUltimateAvailable;
    [HideInInspector] public bool isUltimateActive;

    public abstract void UltimateAction();
    protected abstract void UltimateCooldown();
    protected abstract void UltimateDuration();


    public float GetManaCost()
    {
        return ultimateCost;
    }
}
