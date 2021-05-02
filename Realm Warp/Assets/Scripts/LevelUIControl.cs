using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUIControl : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject defeatMenu;

    private bool isPaused = false;
    private bool isControlMenuOn = false;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the escape key is pressed, the game is paused and the pause menu is enabled.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseMenuOn();
            }
            else
            {
                PauseMenuOff();
            }
        }

        // if the K key is pressed, the game opens the controls menu.
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isControlMenuOn)
            {
                controlsMenu.SetActive(true);
                isControlMenuOn = !isControlMenuOn;
            }
            else
            {
                controlsMenu.SetActive(false);
                isControlMenuOn = !isControlMenuOn;
            }
        }

        // if the player is defeated, the game is paused and the defeat screen appears.
        if (GameInformation.instance.GetPlayer().GetComponent<Player>().GetIsDead() && !isDead)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            defeatMenu.SetActive(true);
            isDead = true;
        }
    }

    public void BackToMenu()
    {
        // due to my implementation I have to force disable realm warp when I go back to menu to prevent bugs.
        GameInformation.instance.SetRealmWarp(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // toggles pause menu on.
    public void PauseMenuOn()
    {
        isPaused = true;    
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
    }

    // toggles pause menu off.
    public void PauseMenuOff()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
    }

}
