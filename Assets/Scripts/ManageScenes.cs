using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SuburbanLeague");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
        Cursor.lockState = CursorLockMode.None;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public void Quit()
    {
        Debug.Log("Player has quit the game.");
        Application.Quit();
    }
}
