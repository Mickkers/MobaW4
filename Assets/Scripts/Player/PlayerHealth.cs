using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public event EventHandler OnPlayerDeath;

    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private AudioSource takeDamageSound;
    [SerializeField] private AudioSource deathSound;
    [Header("Health")]
    [SerializeField] private float maxHealth;

    private bool isOutsideOfZone = false;

    private bool isAlive;

    public float currHealth;

    public float MaxHealth 
    { 
        get => maxHealth; 
        set 
        {
            maxHealth = value;
            currHealth = maxHealth;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = MaxHealth;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutsideOfZone)
        {
            TakeDamage(GameManager.Instance.zoneDamage * Time.deltaTime, this.gameObject);
        }
        if(currHealth < 0 && isAlive)
        {
            isAlive = false;
            Death();
        }
        currHealth = Mathf.Clamp(currHealth, 0, MaxHealth);
        playerUI.UpdateHealthUI(currHealth, maxHealth);
    }

    private void Death()
    {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        gameObject.tag = "DeadPlayer";
        deathSound.Play();
        Invoke(nameof(GameOver), 1f);
    }

    private void GameOver()
    {
        GameManager.Instance.GameOver(GetComponent<PlayerController>());
    }

    public void TakeDamage(float value, GameObject dealer)
    {
        currHealth -= value;
        takeDamageSound.Play();
    }

    public void HealToFull()
    {
        currHealth = maxHealth;
    }

    public Outline GetOutline()
    {
        return GetComponent<PlayerController>().GetOutline();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out ZoneShrink zone))
        {
            isOutsideOfZone = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ZoneShrink zone))
        {
            isOutsideOfZone = true;
        }
    }
}
