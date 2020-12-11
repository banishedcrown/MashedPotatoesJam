using System;
using UnityEngine;

public enum UpgradeNames { 
    None, 
    Player_Paddle_Size, 
    Player_Speed,
    AI_Player, 
    AI_Enemy, 

    Ball_Speed,
    Ball_Value, //flat increase
    Ball_Multiplier, //similar to rally, but upgrades increase how fast the bounces increase value
    Ball_Rally, //chance to spawn a rally ball, that gains points per bounce. 

    Pong_Instance_Increase,
    Pong_Set_Speed,
    Pong_Score_Limit,
    Pong_Auto_Resart,

    Misc_Loss,
};

[Serializable]
public class UpgradeData
{


    //level of upgrades for each upgrade
    public Upgrade Player_Paddle_Size = new Upgrade(UpgradeNames.Player_Paddle_Size, 1, 0, 3, 0.2f);
    public Upgrade Player_Speed = new Upgrade(UpgradeNames.Player_Speed, 2, 0, 4, 0.05f);
    public Upgrade AI_Player = new Upgrade(UpgradeNames.AI_Player, 10, 0, 0, 0);
    public UpgradeData()
    {

    }

    public Upgrade GetUpgradeByName(UpgradeNames name)
    {
        if (name == this.Player_Paddle_Size.name) 
            return Player_Paddle_Size;
        if (name == this.Player_Speed.name) 
            return Player_Speed;
        if (name == this.AI_Player.name)
            return AI_Player;

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