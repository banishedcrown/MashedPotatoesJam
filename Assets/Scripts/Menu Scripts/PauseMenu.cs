﻿using System.Collections;
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

    private AudioMixer Mixer;

    float PriorTimeScale;
  
    void OnEnable()
    {
        Mixer = GameManager.GetManager().GetAudioMixer();
        float CurrentMaster = 1f;
        float CurrentMusic = 1f;
        float CurrentSFX = 1f;

        Mixer.GetFloat("MasterVol", out CurrentMaster);
        Master.value = Mathf.Pow(10, (CurrentMaster / 20f));

        Mixer.GetFloat("MusicVol", out CurrentMusic);
        Music.value = Mathf.Pow(10, (CurrentMusic / 20f));

        Mixer.GetFloat("SFXVol", out CurrentSFX);
        Effects.value = Mathf.Pow(10, (CurrentSFX / 20f));
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SetMaster()
    {
        float sliderValue = Master.value;
        Mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVol", sliderValue);
    }

    public void SetMusic()
    {
        float sliderValue = Music.value;
        Mixer.SetFloat ("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }

    public void SetSFX()
    {
        float sliderValue = Effects.value;
        Mixer.SetFloat ("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVol", sliderValue);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
