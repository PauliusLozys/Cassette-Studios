using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !Pause.GameIsPaused)
            {
                Resume();
            }
            else
            {
                Stop();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Stop()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        if(!LevelManager.isPlayerDead)
            SaveSystem.SaveLevels();
        Application.Quit();
    }

    public void GoToHub()
    {
        Resume();
        if (!LevelManager.isPlayerDead)
            SaveSystem.SaveLevels();
        SceneManager.LoadScene("HubScene");
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
}
