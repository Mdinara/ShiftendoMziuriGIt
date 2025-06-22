using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Functions : MonoBehaviour
{
    public GameObject PausePanel;

    public static bool Paused;

    void Start()
    {
        Paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && DeathScript.Dying == false) PauseMenu();
    }
    public void PauseMenu()
    {
        Paused = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Paused = false;
        PausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        int nightCounter = PlayerPrefs.GetInt("NightCounter");
        nightCounter -= 1;
        PlayerPrefs.SetInt("NightCounter", nightCounter);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
