using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    [SerializeField] private float splitDistance;

    private Camera cam;
    private Animator anim;
    private Transform playerOne;
    private Transform playerTwo;

    private void Start()
    {
        cam = GetComponent<Camera>();
        anim = GetComponent<Animator>();
        playerOne = PlayerController.PlayerOne.transform;
        playerTwo = PlayerController.PlayerTwo.transform;
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(playerOne.position, playerTwo.position) > splitDistance)
        {
            anim.SetTrigger(AnimationStrings.CamOut);
        }
        else
        {
            anim.SetTrigger(AnimationStrings.CamIn);
        }
    }
}
