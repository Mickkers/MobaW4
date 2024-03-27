using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("HealthUI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBarChar;
    [SerializeField] private Image healthBar;

    [Header("Mana UI Elements")]
    [SerializeField] private Image manaBar;
    [SerializeField] private Image manaBarChar;
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Skill UI Eements")]
    public Image skillIcon;
    public TextMeshProUGUI skillText;

    [Header("Ultimate UI Eements")]
    public Image ultimateIcon;
    public TextMeshProUGUI ultimateText;

    public void UpdateHealthUI(float currHealth, float maxHealth)
    {
        healthText.text = "<mspace=28>" + (int)currHealth + "/" + maxHealth + "</mspace>";
        healthBar.fillAmount = currHealth / maxHealth;
        healthBarChar.fillAmount = currHealth / maxHealth;
    }

    public void UpdateManaUI(float currMana, float maxMana)
    {
        manaText.text = "<mspace=28>" + (int)currMana + "/" + maxMana + "</mspace>";
        manaBar.fillAmount = currMana / maxMana;
        manaBarChar.fillAmount = currMana / maxMana;
    }

    public void UpdateUltUI(float cooldown, float ultimateCooldown)
    {
        ultimateIcon.fillAmount = 1f - cooldown / ultimateCooldown;
        ultimateText.text = "" + Mathf.CeilToInt(cooldown);
    }

    public void ResetUltUI()
    {
        ultimateIcon.fillAmount = 1;
        ultimateText.text = "";
    }

    public void UpdateSkillUI(float cooldown, float skillCooldown)
    {
        skillIcon.fillAmount = 1f - cooldown / skillCooldown;
        skillText.text = "" + (int)cooldown;
    }

    public void ResetSkillUI()
    {
        skillIcon.fillAmount = 1;
        skillText.text = "";
    }
}
