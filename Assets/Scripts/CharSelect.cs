using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
    public static CharSelect Instance;
    public EnumCharType pOneSelection;
    public EnumCharType pTwoSelection;
    public bool selectMenu = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetPOne(bool isMelee)
    {
        if (isMelee)
        {
            pOneSelection = EnumCharType.Melee;
        }
        else
        {
            pOneSelection = EnumCharType.Ranged;
        }
    }
    public void SetPTwo(bool isMelee)
    {
        if (isMelee)
        {
            pTwoSelection = EnumCharType.Melee;
        }
        else
        {
            pTwoSelection = EnumCharType.Ranged;
        }
    }
}
