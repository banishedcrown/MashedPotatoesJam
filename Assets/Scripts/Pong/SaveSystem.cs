﻿using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    [DllImport("__Internal")]
    private static extern void syncSystem();

    static string path = Application.persistentDataPath + "/jpgame_v1_5.data";
    static string settingsPath = Application.persistentDataPath + "/jpsettings_v1_5.settings";

    static string deprecatedWebPath = "/idbfs/81d2821253ddbf7ddd185bb6991530b9/game.data";
    static string deprecatedWebPath2 = "/idbfs/jpgame2.data";
    static string deprecatedPath = Application.persistentDataPath + "/game.data";
    static string deprecatedPath2 = Application.persistentDataPath + "/jpgame.data";
    static bool inUse = false;
    static bool settingsInUse = false;
    public static void initSavePath()
    {
        
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            path = "/idbfs/jpgame_v1_5.data";
            settingsPath = "/idbfs/jpsettings_v1_5.settings";

            if (!File.Exists(path)) //let's try to replace the old save immediately.
            {
                if (File.Exists(deprecatedWebPath))
                {
                    UpdateSave(deprecatedWebPath);
                }
                if (File.Exists(deprecatedWebPath2))
                {
                    UpdateSaveToDouble(deprecatedWebPath2);
                }
            }
        }
        else
        {
            if (!File.Exists(path)) //let's try to replace the old save immediately.
            {
                if (File.Exists(deprecatedPath))
                {
                    UpdateSave(deprecatedPath);
                }
                if (File.Exists(deprecatedPath2))
                {
                    UpdateSaveToDouble(deprecatedPath2);
                }
            }
        }
        Debug.Log("Set save path to:" + path);
    }

    public static void SaveData(GameData data)
    {
        while (inUse) 
            continue;
        inUse = true;

        GameSaveDataLarge gameSaveData = new GameSaveDataLarge(data);

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        formatter.Serialize(stream, gameSaveData);
        stream.Close();
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            syncSystem();
        }
        inUse = false;
    }

    public static void SaveData(GameSaveData data)
    {
        while (inUse)
            continue;
        inUse = true;

        GameSaveDataLarge gameSaveData = new GameSaveDataLarge(data);

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        formatter.Serialize(stream, gameSaveData);
        stream.Close();
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            syncSystem();
        }
        inUse = false;
    }


    public static GameData LoadData()
    {
        Debug.Log("loading from path: " + path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameSaveDataLarge data = formatter.Deserialize(stream) as GameSaveDataLarge;

            GameData loadedData = new GameData(data);

            stream.Close();
            return loadedData;
        }
        else
        {
            Debug.LogError("Tried to load with no saveData");
            return null;
        }
    }


    public static void SaveSettings(SettingsData data)
    {
        while (settingsInUse)
            continue;
        settingsInUse = true;

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(settingsPath, FileMode.OpenOrCreate);

        formatter.Serialize(stream, data);
        stream.Close();
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            syncSystem();
        }
        settingsInUse = false;
    }

    public static SettingsData LoadSettings()
    {
        Debug.Log("loading settings from path: " + settingsPath);
        if (File.Exists(settingsPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(settingsPath, FileMode.Open);
            SettingsData data = formatter.Deserialize(stream) as SettingsData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Tried to load with no Settings, creating defaults");
            SettingsData settings = new SettingsData(GameManager.GetManager().audioMixer);
            SaveSettings(settings);
            return settings;
        }
    }

    public static void UpdateSave(string outDatedPath)
    {
        Debug.Log("Updating SaveFile from path: " + outDatedPath);
        if (File.Exists(outDatedPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(outDatedPath, FileMode.Open);
            GameData loadedData = formatter.Deserialize(stream) as GameData;

            stream.Close();

            SaveData(loadedData);
        }
        else
        {
            Debug.LogError("Tried to update Data with no saveData");
        }
    }

    public static void UpdateSaveToDouble(string outDatedPath)
    {
        Debug.Log("Updating SaveFile from path: " + outDatedPath);
        if (File.Exists(outDatedPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(outDatedPath, FileMode.Open);
            GameSaveData loadedData = formatter.Deserialize(stream) as GameSaveData;

            stream.Close();

            SaveData(loadedData);
        }
        else
        {
            Debug.LogError("Tried to update Data with no saveData");
        }
    }

    public static bool SaveExists()
    {
        return File.Exists(path);
    }

    public static byte[] GetSaveData()
    {
        FileStream stream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[stream.Length];
        int num = (int)stream.Length;
        int numRead = 0;
        while(num > 0)
        {
            int n = stream.Read(data, numRead, num);
            if (n == 0) break;

            numRead += n;
            num -= n;
        }
        stream.Close();
        return data;
    }
}
