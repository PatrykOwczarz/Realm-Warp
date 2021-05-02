using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Main menu implementation based on the following guide:
// https://www.youtube.com/watch?v=zc8ac_qUXQY
// the functions used in this script are accessed via the OnClick() events of the UI in the Unity inspector.
public class MainMenuUI : MonoBehaviour
{
    // loads the demo level.
    public void PlayDemoLevel()
    {
        SceneManager.LoadScene(1);
    }
    // loads the survival mode level.
    public void PlaySurvivalLevel()
    {
        SceneManager.LoadScene(2);
    }

    // closes the game when the quit button is pressed.
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
