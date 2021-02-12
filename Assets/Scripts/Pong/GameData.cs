using System;

[Serializable]
public class GameData
{
    public double currentPB = 0;
    public double totalPB = 0;

    public double currentWins = 0;
    public double totalWins = 0;

    public int prestigeLevel = 0;
    public UpgradeData upgrades;
    public ProgressData progress;
    public SettingsData settings;

    public GameData(UpgradeData upgrades)
    {
        this.upgrades = upgrades;
        this.progress = new ProgressData();
        this.settings = new SettingsData(GameManager.GetManager().audioMixer);
    }

    public GameData(UpgradeData upgrades, ProgressData progress, SettingsData settings)
    {
        this.upgrades = upgrades;
        this.progress = progress;
        this.settings = settings;
    }

    public GameData(GameSaveDataLarge data)
    {
        this.currentPB = data.currentPB;
        this.totalPB = data.totalPB;
        this.currentWins = data.currentWins;
        this.totalWins = data.totalWins;

        this.prestigeLevel = data.prestigeLevel;
        this.progress = data.progress;
        this.settings = data.settings;

        this.upgrades = new UpgradeData();

        for (int c = 0; c < data.upgrades.GetLength(0); c++)
        {
            Upgrade theUpgrade = this.upgrades.GetUpgradeByName((UpgradeNames)c + 1);
            theUpgrade.stacks = data.upgrades[c, 0];
            theUpgrade.UpdateCurrentCost();
        }

    }
}
