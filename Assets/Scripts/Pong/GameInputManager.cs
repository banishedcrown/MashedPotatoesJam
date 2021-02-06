using UnityEngine;
using System.Collections.Generic;
using System;

public static class GameInputManager
{
    static Dictionary<string, KeyCode> keyMapping;
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
        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyMapping.Add(keyMaps[i], defaults[i]);
        }
    }

    public static void SetKeyMap(string keyMap, KeyCode key)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMapping[keyMap] = key;
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
        if (keyMap == "Up") defKey = Input.GetAxis("Vertical") > 0;
        if (keyMap == "Down") defKey = Input.GetAxis("Vertical") < 0;
        return Input.GetKeyDown(keyMapping[keyMap]) || defKey;
    }
}