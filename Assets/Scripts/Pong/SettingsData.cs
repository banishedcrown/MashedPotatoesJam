using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;


[Serializable]
public class SettingsData
{
    public float AudioMaster = 0f;
    public float AudioMusic = 0f;
    public float AudioSFX = 0f;

    public KeyCode upKey = KeyCode.None;
    public KeyCode downKey = KeyCode.None;

    public SettingsData(AudioMixer mixer)
    {
        mixer.GetFloat("MasterVol", out AudioMaster);
        mixer.GetFloat("MusicVol", out AudioMusic);
        mixer.GetFloat("SFXVol", out AudioSFX);

        upKey = GameInputManager.GetKeyMap("Up");
        downKey = GameInputManager.GetKeyMap("Down");
        
    }

}