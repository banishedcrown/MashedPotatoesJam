using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GameObject.Find("Options Panel").SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
