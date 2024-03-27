using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MjlSkill : Skill
{
    private PlayerAction pAction;
    private NavMeshAgent agent;

    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private float skillRange;
    [SerializeField] private float skillDamage;
    [SerializeField] private MjlSkillProjectile projectilePrefab;
    public GameObject effect;
    [SerializeField] private AudioSource skillSound;

    private float currDuration;
    private float currCooldown;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pAction = GetComponent<PlayerAction>();
        isSkillActive = false;
        isSkillAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        SkillDuration();
        SkillCooldown();
    }
    public override void SkillAction()
    {
        if (!isSkillActive) return;
        agent.isStopped = true;
        if(pAction.Target != null)
        {
            isSkillActive = false;
            effect.SetActive(true);
            isSkillAvailable = false;
            animator.SetTrigger(AnimationStrings.skillAttack);
            currDuration = skillDuration;
            currCooldown = skillCooldown;
        }
    }

    private void SkillHit()
    {
        if (pAction.Target is null) return;
        skillSound.Play();
        Vector3 pos = transform.position;
        pos.y += 1.2f;
        MjlSkillProjectile proj = Instantiate(projectilePrefab, pos, transform.rotation);
        proj.dealer = GetComponentInParent<PlayerController>().gameObject;
        proj.damage = pAction.GetDamage() * skillDamage;
        proj.range = skillRange;
        Transform targetTransform = pAction.Target.transform;
        targetTransform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 1.2f, targetTransform.position.z);
        proj.SetTarget(targetTransform);
        effect.SetActive(false);
        agent.isStopped = false;
    }

    protected override void SkillCooldown()
    {
        if (isSkillActive || isSkillAvailable)
        {
            return;
        }
        if (currCooldown > 0f)
        {
            currCooldown -= Time.deltaTime;
            playerUI.UpdateSkillUI(currCooldown, skillCooldown);
        }
        else if (currCooldown <= 0f)
        {
            playerUI.ResetSkillUI();

            isSkillAvailable = true;
            currCooldown = 0f;
        }

    }

    protected override void SkillDuration()
    {
        if (!isSkillActive || isSkillAvailable)
        {
            return;
        }
        if (currDuration > 0f)
        {
            currDuration -= Time.deltaTime;
        }
        else if (currDuration <= 0f)
        {
            isSkillActive = false;
            currDuration = 0f;
        }
    }
}
