using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public long currentPB = 0;
    public long totalPB = 0;

    public long currentWins = 0;
    public long totalWins = 0;

    public int prestigeLevel = 0;
    public UpgradeData upgrades;
    public ProgressData progress;

    public GameData(UpgradeData upgrades)
    {
        this.upgrades = upgrades;
    }

    public GameData(UpgradeData upgrades, ProgressData progress)
    {
        this.upgrades = upgrades;
        this.progress = progress;
    }
}
