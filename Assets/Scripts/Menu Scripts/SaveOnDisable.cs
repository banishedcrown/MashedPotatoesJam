using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SaveOnDisable : MonoBehaviour
{
    public AudioMixer Mixer;

    void OnDisable()
    {
        float CurrentMaster = 1f;
        float CurrentMusic = 1f;
        float CurrentSFX = 1f;

        Mixer.GetFloat("MasterVol", out CurrentMaster);
        Mixer.GetFloat("MusicVol", out CurrentMusic);
        Mixer.GetFloat("SFXVol", out CurrentSFX);

        SettingsData settings = GameManager.GetManager().GetSettings();

        settings.AudioMaster = CurrentMaster;
        settings.AudioMusic = CurrentMusic;
        settings.AudioSFX = CurrentSFX;

        SaveSystem.SaveSettings(GameManager.GetManager().GetSettings());

    }
}
