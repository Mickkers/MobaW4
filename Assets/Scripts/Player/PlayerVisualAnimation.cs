using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualAnimation : MonoBehaviour
{
    [SerializeField] private PlayerHealth pHealth;
    [SerializeField] private PlayerMovement pMovement;

    [SerializeField] private float motionSmoothTime;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pHealth.OnPlayerDeath += PHealth_OnPlayerDeath;
        pMovement.OnPlayerMovement += PMovement_OnPlayerMovement;
    }

    private void PMovement_OnPlayerMovement(object sender, PlayerMovement.OnPlayerMovementEventArgs e)
    {

        animator.SetFloat(AnimationStrings.speed, e.speed, motionSmoothTime, Time.deltaTime);
    }

    private void PHealth_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetTrigger(AnimationStrings.death);
    }

    
}
