using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceNavigation : MonoBehaviour
{
    GameManager manager;
    GameObject instances;
    GameObject leftButton;
    GameObject rightButton;
    GameObject original;
    // Start is called before the first frame update
    private void Start()
    {
        manager = GameManager.GetManager();
        instances = GameObject.Find("Instances");
        leftButton = transform.Find("Left").gameObject;
        rightButton = transform.Find("Right").gameObject;
        original = instances.transform.Find("Pong Game").gameObject;
    }

    private void FixedUpdate()
    {
        Upgrade InstanceUpgrade = manager.GetData().upgrades.Pong_Instance_Increase;
        if (InstanceUpgrade.stacks > 0)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            float x = original.transform.localPosition.x;
            if (x == (-20 * InstanceUpgrade.stacks))
            {
                rightButton.SetActive(false);
            }
            if (x == 0)
            {
                leftButton.SetActive(false);
            }
        }
        else
        {
            leftButton.SetActive(false);
            rightButton.SetActive(false);
        }
    }

    public void ChangeInstance(int dir) //-1 left, 1 right
    {
        if (dir < 0) dir = -1;
        if (dir > 0) dir = 1;
        if (dir == 0) return;

        foreach(Transform t in instances.transform)
        {
            if (t.name.StartsWith("Pong"))
            {
                Vector3 pos = t.position;
                pos.x += 20f * -dir;
                t.position = pos;
            }
        }
    }
}
