using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPlayerCharSelectUI : MonoBehaviour
{
    [SerializeField] private bool isPOne;
    [SerializeField] private Image rangeBtn;
    [SerializeField] private Image meleeBtn;
    [SerializeField] private GameObject rangeChar;
    [SerializeField] private GameObject meleeChar;

    private void Update()
    {
        if (isPOne)
        {
            UpdateUI(CharSelect.Instance.pOneSelection);
        }
        else
        {
            UpdateUI(CharSelect.Instance.pTwoSelection);
        }
    }

    private void UpdateUI(EnumCharType selected)
    {
        if(selected == EnumCharType.Melee)
        {
            meleeBtn.color = Color.green;
            meleeChar.SetActive(true);

            rangeBtn.color = Color.white;
            rangeChar.SetActive(false);
        }
        else
        {
            rangeBtn.color = Color.green;
            rangeChar.SetActive(true);

            meleeBtn.color = Color.white;
            meleeChar.SetActive(false);
        }
    }

}
