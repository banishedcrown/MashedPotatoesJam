using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsOffBandaid : MonoBehaviour
{
    void LateUpdate()
    {
        GameObject g = GameObject.Find("Options Panel");
        if (g != null)
            g.SetActive(false);
    }
}
