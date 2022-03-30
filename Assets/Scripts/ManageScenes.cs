using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{

    public static bool isPaused = false;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    //public GameObject gameUI;


    // Once we press the escape button, we check to see if we are paused or not and if so,
    // we display and/or close the pause menu
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Play()
    {
        StartCoroutine(WaitForLoad());
        Time.timeScale = 1f;
        SceneManager.LoadScene("SuburbanLeague");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // We want to resume the game as normal
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    // Once we press escape, we pause the scene and freeze everything so that we can navigate the menu
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        isPaused = true;
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = false;
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        WaitForLoad();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        WaitForLoad();
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
        Time.timeScale = 1f;
        WaitForLoad();
    }

    // Allows the user to quit the game
    public void Quit()
    {
        Debug.Log("Player has quit the game.");
        Application.Quit();
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(3);
    }

}
