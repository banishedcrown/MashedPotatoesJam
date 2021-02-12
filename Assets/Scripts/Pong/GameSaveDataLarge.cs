using System;
using System.Diagnostics;
using UnityEditor;

[Serializable]
public class GameSaveDataLarge
{
    public float version = 1.5f;
    public double currentPB = 0;
    public double totalPB = 0;

    public double currentWins = 0;
    public double totalWins = 0;

    public int prestigeLevel = 0;

    public int[,] upgrades = new int[(int)UpgradeNames.LengthTracker-1, 1];

    public ProgressData progress;
    public SettingsData settings;

    public GameSaveDataLarge(GameData data)
    {
        this.currentPB = data.currentPB;
        this.totalPB = data.totalPB;
        this.currentWins = data.currentWins;
        this.totalWins = data.totalWins;

        this.prestigeLevel = data.prestigeLevel;
        this.progress = data.progress;
        
        for(int c = 0; c < upgrades.GetLength(0); c++)
        {
            upgrades[c, 0] = data.upgrades.GetUpgradeByName((UpgradeNames)c+1).stacks;
        }

    }

    public GameSaveDataLarge(GameSaveData data)
    {
        this.currentPB = data.currentPB;
        this.totalPB = data.totalPB;
        this.currentWins = data.currentWins;
        this.totalWins = data.totalWins;

        this.prestigeLevel = data.prestigeLevel;
        this.progress = data.progress;
        this.settings = new SettingsData(GameManager.GetManager().audioMixer);

        for (int c = 0; c < upgrades.GetLength(0); c++)
        {
            upgrades[c, 0] = data.upgrades[c, 0];
        }

    }
}
