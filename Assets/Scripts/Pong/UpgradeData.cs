using System;
using UnityEngine;

public enum UpgradeNames { None, PaddleSize, PlayerSpeed };

[Serializable]
public class UpgradeData
{


    //level of upgrades for each upgrade
    public Upgrade PaddleSize;
    public Upgrade PlayerSpeed;

    public UpgradeData()
    {
        PaddleSize = new Upgrade(UpgradeNames.PaddleSize, 1, 0, 3, 0.2f);
        PlayerSpeed = new Upgrade(UpgradeNames.PlayerSpeed, 2, 0, 4, 0.05f);
    }

    public Upgrade GetUpgradeByName(UpgradeNames name)
    {
        if (name == this.PaddleSize.name) 
            return PaddleSize;
        if (name == this.PlayerSpeed.name) 
            return PlayerSpeed;

        return null;
    }
}
[Serializable]
public class Upgrade
{
    public UpgradeNames name = UpgradeNames.None;
    public long base_cost; //base cost
    public long current_cost;
    public int stacks; //number of times upgraded
    public int rateIncrease; //rate of increase per stack.
    public float increaseValue;

    public Upgrade(UpgradeNames name, long basecost, int stacks, int rateIncrease, float increaseValue = 0.1f)
    {
        this.name = name;
        base_cost = basecost;
        this.stacks = stacks;
        this.rateIncrease = rateIncrease;
        this.increaseValue = increaseValue;
        current_cost = CalcCurrentCost();
    }

    public void addStack(int num)
    {
        stacks += num;
        current_cost = CalcCurrentCost();
    }


    long CalcCurrentCost()
    {

        return base_cost * ((long)Math.Pow(rateIncrease,stacks));
    }

}