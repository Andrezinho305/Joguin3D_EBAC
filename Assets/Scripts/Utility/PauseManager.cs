using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using Core.StateMachine;

public class PauseManager : Singleton<PauseManager>
{





    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }


}
