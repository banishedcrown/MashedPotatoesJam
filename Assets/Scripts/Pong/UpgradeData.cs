﻿using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeNames { 
    None, 
    Player_Paddle_Size,  
    Player_Speed, 
    AI_Player,  
    AI_Enemy,  

    Ball_Speed,  
    Ball_Value, //flat increase
    Rally_Multiplier, //similar to rally, but upgrades increase how fast the bounces increase value
    Rally_Ball, //chance to spawn a rally ball, that gains points per bounce. 

    Pong_Set_Speed,
    Pong_Auto_Resart,
    Pong_Score_Limit,
    //done^^

    Pong_Instance_Increase,
    Secret_Upgrade, //loss
    Unlock_Music, 

    LengthTracker,
};

public enum BallTypes
{
    NORMAL,
    RALLY,
    SPLIT,

};

[Serializable]
public class UpgradeData
{
    //level of upgrades for each upgrade
    public Upgrade Player_Paddle_Size = new Upgrade(UpgradeNames.Player_Paddle_Size, 1, 2, 0.2f);
    public Upgrade Player_Speed = new Upgrade(UpgradeNames.Player_Speed, 2, 3, 0.05f);
    public Upgrade AI_Player = new Upgrade(UpgradeNames.AI_Player, 25, 10, 0.5f, 3);
    public Upgrade AI_Enemy = new Upgrade(UpgradeNames.AI_Enemy, 10, 5, 0.5f);

    public Upgrade Ball_Speed = new Upgrade(UpgradeNames.Ball_Speed, 10, 4, 0.25f);
    public Upgrade Ball_Value = new Upgrade(UpgradeNames.Ball_Value, 25, 4, 1, 1);
    public Upgrade Ball_Multiplier = new Upgrade(UpgradeNames.Rally_Multiplier, 20, 1.8f, 1, 10, 1);
    public Upgrade Ball_Rally = new Upgrade(UpgradeNames.Rally_Ball, 10, 3, 0.10f,10);

    public Upgrade Pong_Instance_Increase = new Upgrade(UpgradeNames.Pong_Instance_Increase, 250, 10, 1, 5);
    public Upgrade Pong_Set_Speed = new Upgrade(UpgradeNames.Pong_Set_Speed, 10, 4, 0.25f);
    public Upgrade Pong_Score_Limit = new Upgrade(UpgradeNames.Pong_Score_Limit, 10, 4, 5);
    public Upgrade Pong_Auto_Resart = new Upgrade(UpgradeNames.Pong_Auto_Resart, 200, 0, 0, 7);

    public Upgrade Unlock_Music = new Upgrade(UpgradeNames.Unlock_Music, 20, 0, 0, 1);

    public Upgrade Misc_Loss = new Upgrade(UpgradeNames.Secret_Upgrade, 10000, 0, 0, 100);

    public UpgradeData()
    {
       
    }

    public Upgrade GetUpgradeByName(UpgradeNames name)
    {
        Upgrade[] upgrades = {
            Player_Paddle_Size, 
            Player_Speed,
            AI_Player,
            AI_Enemy,
            Ball_Speed,
            Ball_Value,
            Ball_Multiplier,
            Ball_Rally,
            Pong_Instance_Increase,
            Pong_Set_Speed,
            Pong_Score_Limit,
            Pong_Auto_Resart,
            Misc_Loss,
            Unlock_Music
         };

        foreach(Upgrade u in upgrades)
        {
            if (name == u.name) return u;
        }
        return null;
    }
}
[Serializable]
public class Upgrade
{
    public UpgradeNames name = UpgradeNames.None;
    public double base_cost; //base cost
    public double current_cost;
    public int stacks; //number of times upgraded
    public float rateIncrease; //rate of increase per stack.
    public float increaseValue;
    public int winsRequired;


    private double initRallyMValue = 1;
    private double secondRallyMValue = 0;
    private double currentRallyMValue = 1;
    private int lastRallyStack = 0;

    public Upgrade(UpgradeNames name, double basecost, float rateIncrease, float increaseValue = 0.1f, int winsRequired = 0, int defaultstacks = 0)
    {
        this.name = name;
        base_cost = basecost;
        this.stacks = defaultstacks;
        this.rateIncrease = rateIncrease;
        this.increaseValue = increaseValue;
        this.winsRequired = winsRequired;
        UpdateCurrentCost();
    }

    public void addStack(int num)
    {
        stacks += num;
        UpdateCurrentCost();
    }


    public double CalcCurrentCost()
    {
        if (name == UpgradeNames.Ball_Value)
        {
            return (double)(Math.Pow(2,stacks)*(5*stacks + 4));
        }
        
        return base_cost * ((double)Math.Pow(rateIncrease, stacks));
    }

    public void UpdateCurrentCost()
    {
        this.current_cost = CalcCurrentCost();

        if (name == UpgradeNames.Rally_Multiplier)
        {
            double temp = initRallyMValue;
            double temp2 = secondRallyMValue;

            for(int c = 0; c < stacks; c++)
            {
                currentRallyMValue = temp + temp2;
                temp = temp2;
                temp2 = currentRallyMValue;
            }
            Debug.Log("Rally Ball initialized to: " + currentRallyMValue + ", stacks: " + stacks);
            //fallthrough intended
        }
    }

    public double GetRallyValue()
    {
        if(name == UpgradeNames.Rally_Multiplier)
        {
            return currentRallyMValue;
        }
        return -10000000000000;
    }

}