using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayDemoLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void PlaySurvivalLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
