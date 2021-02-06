using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;


[Serializable]
class SettingsData
{
    float AudioMaster = 1f;
    float AudioMusic = 1f;
    float AudioSFX = 1f;

    KeyCode upkey = KeyCode.W;
    KeyCode downKey = KeyCode.S;

    public SettingsData(AudioMixer mixer)
    {
        mixer.GetFloat("MasterVol", out AudioMaster);
        mixer.GetFloat("MusicVol", out AudioMusic);
        mixer.GetFloat("SFXVol", out AudioSFX);

        upkey = GameInputManager.GetKeyMap("Up");
        downKey = GameInputManager.GetKeyMap("Down");
        
    }

}