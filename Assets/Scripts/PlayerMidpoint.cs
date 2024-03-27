using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMidpoint : MonoBehaviour
{
    private Transform playerOne;
    private Transform playerTwo;

    private void Start()
    {
        playerOne = PlayerController.PlayerOne.transform;
        playerTwo = PlayerController.PlayerTwo.transform;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(playerOne.position, playerTwo.position, .5f);
    }
}
