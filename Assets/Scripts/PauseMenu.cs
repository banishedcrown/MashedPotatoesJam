using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public Slider Master;
    public Slider Music;
    public Slider Effects;

    public AudioMixer MasterVol;

    void Start()
    {
        Master.value = PlayerPrefs.GetFloat("Master", 1f);
        Music.value = PlayerPrefs.GetFloat("Music", 1f);
        Effects.value = PlayerPrefs.GetFloat("SFX", 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void SetMaster()
    {
        float sliderValue = Master.value;
        MasterVol.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVol", sliderValue);
    }

    public void SetMusic()
    {
        float sliderValue = Music.value;
        MasterVol.SetFloat ("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }

    public void SetSFX()
    {
        float sliderValue = Effects.value;
        MasterVol.SetFloat ("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVol", sliderValue);
    }
}
