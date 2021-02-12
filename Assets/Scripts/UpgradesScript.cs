using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesScript : MonoBehaviour
{
    public GameObject[] Prompt;

    void Start()
    {
        GameManager m = GameManager.GetManager();
        var p = m.GetData().progress;

        int core = p.coreLevel;
        int bits = p.bitLevel;

        /*Defines the point in the array of gameobjects the game takes, 0 is errors, 1 is the start, 2 is the 2nd, 3 is when you can still gain both cores 
        and bytes, 4 is when you can upgrade only cores, 5 is when you can upgrade only bytes, 6 is balanced victory, 7 is byte favored victory,
        and 8 is core favored victory, and 9 is the final victory*/
        int num;
        //Bitandcore is formatted so that bitlevel takes the 10's place and corelevel takes the 1's place.
        int bitandcore = bits * 10 + core;


        switch (bitandcore)
        {
            case 44:
                num = 9;
                break;

            case 43:
                num = 7;
                break;

            case 42:
                num = 4;
                break;

            case 41:
                num = 4;
                break;

            case 34:
                num = 8;
                break;

            case 33:
                num = 6;
                break;

            case 32:
                num = 3;
                break;

            case 31:
                num = 3;
                break;

            case 24:
                num = 5;
                break;

            case 23:
                num = 3;
                break;

            case 22:
                num = 3;
                break;

            case 21:
                num = 3;
                break;

            case 14:
                num = 5;
                break;

            case 13:
                num = 3;
                break;

            case 12:
                num = 3;
                break;

            case 11:
                num = 3;
                break;

            case 10:
                num = 2;
                break;

            case 0:
                num = 1;
                break;

            default:
                num = 0;
                break;

        }
        Prompt[num].SetActive(true);

    }

    public void CoreIncrease(int level)
    {
        GameManager.GetManager().GetData().progress.AddCoreLevel(level);
        
        GameManager m = GameManager.GetManager();

        m.GetData().currentPB /= 2;

    }

    public void BitsIncrease(int level)
    {
        GameManager.GetManager().GetData().progress.AddBitLevel(level);
    }

}
