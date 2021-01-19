using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject Version;
    public GameObject Platform;
    public GameObject MenuPassword;
    public GameObject MenuNoPassword;

    //For Franz
    public bool OverrideActivated;
    public string VersionOverride;
    public string PlatformOverride;

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        //Debug.Log(Application.platform);
        //Debug.Log(Application.version);

        //Check the runtime platform, output the platform into the platform gameobject, then set the 2 gameobjects with menu elements to true or false.
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Windows";
            MenuPassword.SetActive(false);
            MenuNoPassword.SetActive(true);
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Web";
            MenuPassword.SetActive(true);
            MenuNoPassword.SetActive(false);
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Mac";
            MenuPassword.SetActive(false);
            MenuNoPassword.SetActive(true);
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Linux";
            MenuPassword.SetActive(false);
            MenuNoPassword.SetActive(true);
        }
        else
        {
            //If all checks fail just push the application platform unformatting and set to the windows version of the main menu.
            Platform.GetComponent<TMP_Text>().text = Application.platform.ToString();
            MenuPassword.SetActive(false);
            MenuNoPassword.SetActive(true);
        }


        //Then set the application version text to the current version.
        Version.GetComponent<TMP_Text>().text = "V" + Application.version;


        //Override Stuff
        if (OverrideActivated == true)
        {
            Platform.GetComponent<TMP_Text>().text = PlatformOverride;
            Version.GetComponent<TMP_Text>().text = VersionOverride;
        }
    }
}
