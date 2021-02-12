using UnityEngine;
using System.Collections.Generic;
using System;

public static class GameInputManager
{
    static Dictionary<string, KeyCode> keyMapping;
    public static Dictionary<string, KeyCode> keyDefaults;
    static string[] keyMaps = new string[2]
    {
        "Up",
        "Down",
    };
    static KeyCode[] defaults = new KeyCode[2]
    {
        KeyCode.W,
        KeyCode.S,
    };

    static GameInputManager()
    {
        InitializeDictionary();
    }

    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<string, KeyCode>();
        keyDefaults = new Dictionary<string, KeyCode>();
        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyMapping.Add(keyMaps[i], defaults[i]);
            keyDefaults.Add(keyMaps[i], defaults[i]);
        }
    }

    public static void SetKeyMap(string keyMap, KeyCode key)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMapping[keyMap] = key;

        GameManager m = GameManager.GetManager();
        GameData data = m.GetData();
        if (keyMap == keyMaps[0])
        {
            data.settings.upKey = key;
        }
        else if (keyMap == keyMaps[1])
        {
            data.settings.downKey = key;
        }
    }

    public static KeyCode GetKeyMap(string keyMap)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        return keyMapping[keyMap];
    }

    public static bool GetKeyDown(string keyMap)
    {
        bool defKey = false;
        if (keyMap == keyMaps[0]) defKey = Input.GetAxis("Vertical") > 0;
        if (keyMap == keyMaps[1]) defKey = Input.GetAxis("Vertical") < 0;
        return Input.GetKeyDown(keyMapping[keyMap]) || defKey;
    }
}