using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject Version;
    public GameObject Platform;

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        Debug.Log("Start");
        Debug.Log(Application.platform);
        Debug.Log(Application.version);

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Windows";
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Web";
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Mac";
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Linux";
        }
        else
        {
            Platform.GetComponent<TMP_Text>().text = " ";
        }

        Version.GetComponent<TMP_Text>().text = "V" + Application.version;

    }
}
