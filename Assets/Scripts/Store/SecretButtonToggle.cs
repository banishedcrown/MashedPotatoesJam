using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecretButtonToggle : MonoBehaviour
{
    // Start is called before the first frame update

    public string SecretScene;
    public GameObject DefaultObject;

    private bool secretOn = false;

    private TMP_Text label;
    private Button button;

    void Start()
    {
        label = transform.Find("Label").gameObject.GetComponent<TMP_Text>();
        button = GetComponent<Button>();
        button.onClick.AddListener(clicked);
    }


    void clicked()
    {
        if (secretOn)
        {
            SceneManager.UnloadSceneAsync(SecretScene);
            DefaultObject.SetActive(true);
            secretOn = false;
            label.text = "Secret On";
        }
        else
        {
            SceneManager.LoadScene(SecretScene, LoadSceneMode.Additive);
            DefaultObject.SetActive(false);
            secretOn = true;
            label.text = "Secret Off";
        }
    }

}
