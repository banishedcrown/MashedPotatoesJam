using System;

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

    public GameData(GameSaveData data)
    {
        this.currentPB = data.currentPB;
        this.totalPB = data.totalPB;
        this.currentWins = data.currentWins;
        this.totalWins = data.totalWins;

        this.prestigeLevel = data.prestigeLevel;
        this.progress = data.progress;

        this.upgrades = new UpgradeData();

        for (int c = 0; c < data.upgrades.GetLength(0); c++)
        {
            Upgrade theUpgrade = this.upgrades.GetUpgradeByName((UpgradeNames)c + 1);
            theUpgrade.stacks = data.upgrades[c, 0];
            theUpgrade.UpdateCurrentCost();
        }

    }
}
