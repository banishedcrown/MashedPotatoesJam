using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public long currentPB = 0;
    public long totalPB = 0;
    public int prestigeLevel = 0;
    public UpgradeData upgrades;

    public GameData(UpgradeData upgrades)
    {
        this.upgrades = upgrades;
    }

    public void LoadData()
    {

    }

}
