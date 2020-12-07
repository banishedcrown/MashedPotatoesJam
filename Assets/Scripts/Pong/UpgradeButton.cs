using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    GameObject managerObject;
    GameManager manager;
    Button button;
    Upgrade theUpgrade;

    public GameObject costLabel; 
    public UpgradeNames upgradeName;

    private void Start()
    {
        managerObject = GameObject.FindGameObjectWithTag("Manager");
        manager = managerObject.GetComponent<GameManager>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        theUpgrade = manager.GetData().upgrades.GetUpgradeByName(upgradeName);

        if(costLabel == null)
        {
            transform.Find("Cost");
        }
    }
    private void Update()
    {
        if(manager.GetData().currentPB < theUpgrade.current_cost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }

        costLabel.GetComponent<Text>().text = theUpgrade.current_cost + " PB";
    }
   /* public void PushedButton(UpgradeNames name, int value)
    {
        UpgradeData d = manager.GetComponent<GameManager>().GetData().upgrades;

        Upgrade u = d.GetUpgradeByName(name);
        u.addStack(value);

        
    }*/

    void Clicked()
    {
        theUpgrade.addStack(1);
    }
    
}
