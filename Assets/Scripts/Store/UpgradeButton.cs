using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    GameObject managerObject;
    GameManager manager;
    Button button;
    Upgrade theUpgrade;

    public GameObject costLabel;
    public GameObject nameLabel;
    public UpgradeNames upgradeName;

    public GameObject toggleButton;
    public GameObject pongPrefab;

    GameData gameData;

    GameObject[] pongManagers;
    int currentScoreLimit = 0;
    GameObject minus, plus;

    private void Start()
    {
        managerObject = GameObject.FindGameObjectWithTag("Manager");
        manager = managerObject.GetComponent<GameManager>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        theUpgrade = manager.GetData().upgrades.GetUpgradeByName(upgradeName);

        if(costLabel == null)
        {
            costLabel = transform.Find("Cost").gameObject;
        }
        if(nameLabel == null)
        {
            nameLabel = transform.Find("Label").gameObject;
            nameLabel.GetComponent<TMP_Text>().text = theUpgrade.name.ToString().Replace('_', ' ');
        }
        gameData = manager.GetData();
        UpdateUgrades();

        if (theUpgrade.name == UpgradeNames.Pong_Score_Limit)
        {
            pongManagers = GameObject.FindGameObjectsWithTag("PongGame");
            minus = transform.Find("Minus").gameObject;
            plus = transform.Find("Plus").gameObject;
        }
    }
    private void Update()
    {
        //print(theUpgrade + "," + gameObject.name + ", " + upgradeName);
        if (gameData.currentPB < theUpgrade.current_cost || gameData.currentWins < theUpgrade.winsRequired)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
        string cost = theUpgrade.current_cost + " PB";
        if(theUpgrade.winsRequired > 0)
        {
            cost += "\n" + theUpgrade.winsRequired + " wins";
        }
        costLabel.GetComponent<TMP_Text>().text = cost;

        if (theUpgrade.name == UpgradeNames.Pong_Score_Limit)
        {
            pongManagers = GameObject.FindGameObjectsWithTag("PongGame");
            minus.SetActive(true);
            plus.SetActive(true);
            if (currentScoreLimit == 0)
            {
                plus.SetActive(false);
            }
            if (currentScoreLimit == -theUpgrade.stacks)
            {
                minus.SetActive(false);
            }
        }
    }
  
    void Clicked()
    {
        manager.RemovePB(theUpgrade.current_cost);
        theUpgrade.addStack(1);
        UpdateUgrades();
    }

    private void OnEnable()
    {
        UpdateUgrades(); 
    }


    void UpdateUgrades()
    {
        if (theUpgrade != null)
        {
            if (theUpgrade.name == UpgradeNames.Secret_Upgrade)
            {
                if (theUpgrade.stacks > 0)
                {
                    toggleButton.SetActive(true);
                }
                else
                {
                    toggleButton.SetActive(false);
                }
            }
            if (theUpgrade.name == UpgradeNames.Pong_Instance_Increase)
            {
                GameManager.InitializeInstances(pongPrefab);
            }
            if (theUpgrade.name == UpgradeNames.Pong_Score_Limit)
            {
                pongManagers = GameObject.FindGameObjectsWithTag("PongGame");
            }
        }
    }

    public void changeScoreLimit( int dir)
    {
        currentScoreLimit += dir;
        if (currentScoreLimit > 0) currentScoreLimit = 0;
        if (currentScoreLimit < -theUpgrade.stacks) currentScoreLimit = -theUpgrade.stacks;

        foreach (GameObject g in pongManagers)
        {
            g.GetComponent<PongManager>().changeScoreLimit(currentScoreLimit);
        }



    }
}
