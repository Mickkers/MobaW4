using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjlUltimate : Ultimate
{
    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private GameObject effect;
    [SerializeField] private float burnDamage;
    [SerializeField] private AudioSource ultimateSound;

    private float currDuration;
    private float currCooldown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isUltimateActive = false;
        isUltimateAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        UltimateDuration();
        UltimateCooldown();
    }
    public override void UltimateAction()
    {
        isUltimateActive = true;
        isUltimateAvailable = false;

        animator.SetTrigger(AnimationStrings.ultimate);
        currDuration = 100f;
        currCooldown = ultimateCooldown;
    }

    private void UltimateHit()
    {
        UltDamage(ultimateDamage);
        ultimateSound.Play();
        currDuration = ultimateDuration;
        effect.SetActive(true);
    }

    protected override void UltimateDuration()
    {
        if (!isUltimateActive)
        {
            return;
        }
        if (currDuration > 0f)
        {
            if(currDuration <= 4f)
            {
                UltDamage(burnDamage * Time.deltaTime);
            }
            currDuration -= Time.deltaTime;
        }
        else if (currDuration <= 0f)
        {
            effect.SetActive(false);

            isUltimateActive = false;
            currDuration = 0f;
        }
    }

    protected override void UltimateCooldown()
    {
        if (isUltimateActive || isUltimateAvailable)
        {
            return;
        }
        if (currCooldown > 0f)
        {
            currCooldown -= Time.deltaTime;
            playerUI.UpdateUltUI(currCooldown, ultimateCooldown);
        }
        else if (currCooldown <= 0f)
        {
            playerUI.ResetUltUI();

            isUltimateAvailable = true;
            currCooldown = 0f;
        }

    }

    private void UltDamage(float damageMultiplier)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ultimateRange);
        float damage = GetComponent<PlayerAction>().GetDamage();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == this.gameObject) return;
            if (hitCollider.TryGetComponent(out ITakeDamage target))
            {
                target.TakeDamage(damageMultiplier * damage, GetComponentInParent<PlayerController>().gameObject);
            }
        }
    }
}

