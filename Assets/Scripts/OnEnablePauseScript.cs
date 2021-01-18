using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnablePauseScript : MonoBehaviour
{
    float PriorTimeScale;

    void OnEnable()
    {
        Pause();
    }

    void Pause()
    {
        PriorTimeScale = Time.timeScale;
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
    }

    public void Resume()
    {
        if (PriorTimeScale == 1)
        {
            Time.timeScale = 1;
        }

        //Otherwise do Nothing

    }
}
