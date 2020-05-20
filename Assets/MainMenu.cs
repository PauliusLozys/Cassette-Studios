using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadHub()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
