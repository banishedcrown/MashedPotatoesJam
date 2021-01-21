using System;
using System.Diagnostics;
using UnityEditor;

[Serializable]
public class GameSaveData
{
    public ulong currentPB = 0;
    public ulong totalPB = 0;

    public ulong currentWins = 0;
    public ulong totalWins = 0;

    public int prestigeLevel = 0;

    public int[,] upgrades = new int[(int)UpgradeNames.LengthTracker-1, 1];

    public ProgressData progress;

    public GameSaveData(GameData data)
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
}
