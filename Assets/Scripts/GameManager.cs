using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float zoneDamage;

    [SerializeField] private TextMeshProUGUI goverHeader;
    [SerializeField] private TextMeshProUGUI goverText;
    [SerializeField] private RectTransform gameoverUI;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI headToFinishText;


    [HideInInspector] public bool isGameover;

    private PlayerController pOne;
    private PlayerController pTwo;
    
    void Awake()
    {
        Instance = this;
        isGameover = false;
    }

    private void Start()
    {
        pOne = PlayerController.PlayerOne;
        pTwo = PlayerController.PlayerTwo;
    }


    public void GameOver(PlayerController loser)
    {

        pOne.enabled = false;
        pTwo.enabled = false;

        goverHeader.text = "Game Over";

        if(loser == pOne)
        {
            goverText.text = "Player Two Wins!";
        }
        else
        {
            goverText.text = "Player One Wins!";
        }

        isGameover = true;
        //Time.timeScale = 0;
        gameoverUI.gameObject.SetActive(true);
    }

    public Transform GetNearestPlayer(Vector3 pos)
    {
        if(Vector3.Distance(pos, pOne.transform.position) < Vector3.Distance(pos, pTwo.transform.position))
        {
            return pOne.transform;
        }
        else
        {
            return pTwo.transform;
        }
    }
}
