using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class PauseManager : Singleton<PauseManager>
{
    public GameObject uiScreen;
    public KeyCode pauseKey = KeyCode.Escape;
    public static bool _isPaused;


    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            uiScreen.SetActive(!uiScreen.activeSelf);
            _isPaused = !_isPaused;
            PauseGame();
        }
    }

    public void PauseGame()
    {
       if(_isPaused)
        {
            Time.timeScale = 0f;
        }

       else
        {
            Time.timeScale = 1;
        }

    }
}
