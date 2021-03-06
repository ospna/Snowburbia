using Assets.Scripts.Entities;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ManageScenes : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject inGameUI;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject finalMenu;
    public GameObject halfTimeMenu;

    public Toggle fsTog;
    //public static Toggle aiTog;

    //Team Owner;

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;
    public TMP_Text resolutionLabel;

    //public GameObject gameUI;

    void Start()
    {
        fsTog.isOn = Screen.fullScreen;
        /*
        if(PlayerPrefs.GetInt("ToggleSelected") == 0)
        {
            aiTog.isOn = true;
            aiTog.isOn = Owner.IsUserControlled == false;
        }
        else
        {
            aiTog.isOn = Owner.IsUserControlled == true;
        }

        // if its toggled on, then its ai versus ai
        aiTog.isOn = true == Owner.IsUserControlled == false;
        */

        bool foundRes = false;
        for(int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].hor && Screen.height == resolutions[i].vert)
            {
                foundRes = true;

                selectedRes = i;

                UpdateResText();
            }
        }

        if(!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.hor = Screen.width;
            newRes.vert = Screen.height;

            resolutions.Add(newRes);
            selectedRes = resolutions.Count - 1;

            UpdateResText();
        }
    }

    public void ApplyGraphics()
    {
        //Screen.fullScreen = fsTog.isOn;

        Screen.SetResolution(resolutions[selectedRes].hor, resolutions[selectedRes].vert, fsTog.isOn);
    }


    // Once we press the escape button, we check to see if we are paused or not and if so,
    // we display and/or close the pause menu
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
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
        SceneManager.LoadScene("WSL");
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // We want to resume the game as normal
    public void Resume()
    {
        pauseMenu.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
        //Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    // Once we press escape, we pause the scene and freeze everything so that we can navigate the menu
    public void Pause()
    {
        pauseMenu.SetActive(true);
        inGameUI.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        inGameUI.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        
        if(isPaused == true)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void Rematch()
    {
        finalMenu.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Forfeit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("WSL");
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        inGameUI.SetActive(false);
        halfTimeMenu.SetActive(false);
    }

    public void ResLeft()
    {
        selectedRes--;
        if(selectedRes < 0)
        {
            selectedRes = 0;
        }
        UpdateResText();
    }

    public void ResRight()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count - 1;
        }
        UpdateResText();
    }

    public void UpdateResText()
    {
        resolutionLabel.text = resolutions[selectedRes].hor.ToString() + " x " + resolutions[selectedRes].vert.ToString();
    }

    // Allows the user to quit the game
    public void Quit()
    {
        Debug.Log("Player has quit the game.");
        Application.Quit();
    }

    [System.Serializable]
    public class ResItem
    {
        public int hor, vert;
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(3);
    }

}
