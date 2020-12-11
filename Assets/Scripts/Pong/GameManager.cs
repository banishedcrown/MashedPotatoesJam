using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameData data;
    UpgradeData upgrades;

    TMP_Text CurrentPBLabel;

    public int alterMoney = 0;
    private void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Manager");

        if (g.Length > 1)
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
        CurrentPBLabel = GameObject.Find("CurrentPB").GetComponent<TMP_Text>();

        GameData loadedData = null;//SaveSystem.LoadData();
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

    private void OnGUI()
    {
        CurrentPBLabel.text = "CURRENT PB: " + data.currentPB;
        if(alterMoney != 0)
        {
            AddPB(alterMoney);
            alterMoney = 0;
        }
    }

    public void AddPB(long value)
    {
        this.data.currentPB += value;
        this.data.totalPB += value;
        Debug.Log("current score: " + this.data.currentPB);
    }

    public void RemovePB(long value)
    {
        this.data.currentPB -= value;
        Debug.Log("current score: " + this.data.currentPB);
    }

    public GameData GetData()
    {
        return this.data;
    }
}
