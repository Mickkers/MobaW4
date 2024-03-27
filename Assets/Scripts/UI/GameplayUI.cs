using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance { get; private set; }

    [SerializeField] private RectTransform pauseMenu;
    [SerializeField] private RectTransform gameplayUI;
    [SerializeField] private RectTransform gameoverMenu;

    public bool isPaused;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }


    public void GoToMainMenu(int val)
    {
        Time.timeScale = 1;
        if (val == 0)
        {
            CharSelect.Instance.selectMenu = true;
        }
        else
        {
            CharSelect.Instance.selectMenu = false;
        }
        SceneManager.LoadScene(0);

    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        isPaused = false;
        pauseMenu.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
