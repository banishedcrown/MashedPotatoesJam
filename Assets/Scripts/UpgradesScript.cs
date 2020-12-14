using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesScript : MonoBehaviour
{
    public GameObject[] Prompt;

    void Start()
    {
        GameManager m = GameManager.GetManager();
        ProgressData p = m.GetData().progress;

        int core = p.coreLevel;
        int bits = p.bitLevel;

        int num = 0;
        int bitandcore = bits * core;

        if ((bitandcore) == 0)
        {
            if ((bits + core) == 0)
            {
                num = 0;
            }
            else
            {
                num = 1;
            }
        }
        else if ((bitandcore) == 1)
        {
            num = 2;
        }
        else if ((bitandcore) == 2)
        {
            num = 3;
        }
        else if ((bitandcore) > 2 && (bitandcore) <= 4 )
        {
            num = 4;
        }
        else if ((bitandcore) > 4 && (bitandcore) <= 6)
        {
            num = 5;
        }
        else if ((bitandcore) > 6 && (bitandcore) <= 8)
        {
            num = 6;
        }
        else if ((bitandcore) > 8)
        {
            num = 7;
        }

        Prompt[num].SetActive(true);

    }

    public void CoreIncrease(int level)
    {
        GameManager.GetManager().GetData().progress.AddCoreLevel(level);
    }

    public void BitsIncrease(int level)
    {
        GameManager.GetManager().GetData().progress.AddBitLevel(level);
    }

}
