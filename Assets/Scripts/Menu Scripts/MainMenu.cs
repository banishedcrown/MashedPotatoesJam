using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Version;
    public GameObject Platform;
    public GameObject MenuWebGl;
    public GameObject MenuNotWeb;
    public GameObject saveOverwritePrompt;

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
            MenuWebGl.SetActive(false);
            MenuNotWeb.SetActive(true);
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Web";
            MenuWebGl.SetActive(true);
            MenuNotWeb.SetActive(false);
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Mac";
            MenuWebGl.SetActive(false);
            MenuNotWeb.SetActive(true);
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Platform.GetComponent<TMP_Text>().text = "Linux";
            MenuWebGl.SetActive(false);
            MenuNotWeb.SetActive(true);
        }
        else
        {
            //If all checks fail just push the application platform unformatting and set to the windows version of the main menu.
            Platform.GetComponent<TMP_Text>().text = Application.platform.ToString();
            MenuWebGl.SetActive(false);
            MenuNotWeb.SetActive(true);
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


    public void NewGame()
    {
        GameManager.GetManager().OverwritePrompt = this.saveOverwritePrompt;
        GameManager.GetManager().NewGame();
    }

    public void LoadGame()
    {
        GameManager.GetManager().LoadGame();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
