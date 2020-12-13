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
        Prompt[num].SetActive(true);
    }

    void CoreIncrease(int level)
    {
        GameManager.GetManager().GetData().progress.AddCoreLevel(level);
    }

    void BitsIncrease(int level)
    {
        GameManager.GetManager().GetData().progress.AddBitLevel(level);
    }

}
