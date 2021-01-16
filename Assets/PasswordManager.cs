using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    // Start is called before the first frame update
    string hexSaveFile;

    GameManager gm;
    public TMP_Text outputButton;
    public TMP_InputField OutputField;
    public TMP_Text inputButton;
    public TMP_InputField InputField;

    public GameObject OverwritePrompt;

    GameData importedData;
 

    void OnEnable()
    {
        byte[] data = SaveSystem.GetSaveData();
        hexSaveFile = System.Convert.ToBase64String(data);

        OutputField.text = hexSaveFile;
    }
    public void PasswordEntered()
    {
        try
        {
            byte[] data = System.Convert.FromBase64String(InputField.text);
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            importedData = formatter.Deserialize(ms) as GameData;

            if (SaveSystem.SaveExists())
            {
                OverwritePrompt.SetActive(true);
            }
            else
            {
                ConfirmedLoad();
            }
        }
        catch
        {
            inputButton.text = "BAD!";
            return;
        }
    }

    public void CopySaveClicked()
    {
        GUIUtility.systemCopyBuffer = hexSaveFile;
        outputButton.text = "Copied to ClipBoard!";
    }

    public void ConfirmedLoad()
    {

        SaveSystem.SaveData(importedData); //overrides save. they better be sure. 
        inputButton.text = "Loaded!";
    }
}
