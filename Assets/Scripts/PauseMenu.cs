using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider Master;
    public Slider Music;
    public Slider Effects;

    public AudioMixer MasterVol;

    float PriorTimeScale;

    void Start()
    {
        Master.value = PlayerPrefs.GetFloat("Master", 1f);
        Music.value = PlayerPrefs.GetFloat("Music", 1f);
        Effects.value = PlayerPrefs.GetFloat("SFX", 1f);
    }
    
    void OnEnable()
    {
        Pause();
    }

    void Pause()
    {
        PriorTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        if (PriorTimeScale == 1)
        {
            Time.timeScale = 1f;
        }

        //Otherwise do Nothing
        
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
