﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameData data;
    UpgradeData upgrades;
    private void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Manager");

        if(g.Length > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        upgrades = new UpgradeData();

        GameData loadedData = SaveSystem.LoadData();
        if (loadedData != null)
        {
            data = loadedData;
            upgrades = data.upgrades;
        }
        else
        {
            data = new GameData(upgrades);
            SaveSystem.SaveData(data);
        }
    }

}
