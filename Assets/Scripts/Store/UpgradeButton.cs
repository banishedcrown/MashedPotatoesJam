﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    GameObject managerObject;
    GameManager manager;
    Button button;
    Upgrade theUpgrade;

    public GameObject costLabel;
    public GameObject nameLabel;
    public UpgradeNames upgradeName;

    GameData gameData;

    private void Start()
    {
        managerObject = GameObject.FindGameObjectWithTag("Manager");
        manager = managerObject.GetComponent<GameManager>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        theUpgrade = manager.GetData().upgrades.GetUpgradeByName(upgradeName);

        if(costLabel == null)
        {
            costLabel = transform.Find("Cost").gameObject;
        }
        if(nameLabel == null)
        {
            nameLabel = transform.Find("Label").gameObject;
            nameLabel.GetComponent<Text>().text = theUpgrade.name.ToString().Replace('_', ' ');
        }
        gameData = manager.GetData();
    }
    private void Update()
    {
        
        if (gameData.currentPB < theUpgrade.current_cost || gameData.currentWins < theUpgrade.winsRequired)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
        string cost = theUpgrade.current_cost + " PB";
        if(theUpgrade.winsRequired > 0)
        {
            cost += "\n" + theUpgrade.winsRequired + " wins";
        }
        costLabel.GetComponent<Text>().text = cost;
    }
   /* public void PushedButton(UpgradeNames name, int value)
    {
        UpgradeData d = manager.GetComponent<GameManager>().GetData().upgrades;

        Upgrade u = d.GetUpgradeByName(name);
        u.addStack(value);

        
    }*/

    void Clicked()
    {
        manager.RemovePB(theUpgrade.current_cost);
        theUpgrade.addStack(1);
    }
    
}