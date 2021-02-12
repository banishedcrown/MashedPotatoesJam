using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameData data;
    UpgradeData upgrades;
    ProgressData progress;

    SettingsData settings;

    TMP_Text CurrentPBLabel;
    TMP_Text CurrentWinLabel;

    public GameObject OverwritePrompt;
    public GameObject pongPrefab;
    public GameObject secretPrefab;

    GameObject OptionsPanel;
    Button loadButton;

    public AudioClip music;
    public AudioMixer audioMixer;

    public double alterMoney = 0;
    public double alterWins = 0;
    bool inGame = false;

    bool EnabledOptions = false;

    private void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Manager");

        if (g.Length > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            SceneManager.sceneLoaded += OnLevelLoaded;
            DontDestroyOnLoad(this.gameObject);
        }
        SaveSystem.initSavePath();

        settings = SaveSystem.LoadSettings();
        UpdateAudio();
        UpdateKeyMaps();
    }

    // Start is called before the first frame update
    void Start()
    {

        OnLevelLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Pong Scene" || scene.name == "Secret Upgrade")
        {
            inGame = true;
            CurrentPBLabel = GameObject.Find("CurrentPB").GetComponent<TMP_Text>();
            CurrentWinLabel = GameObject.Find("CurrentWins").GetComponent<TMP_Text>();
            OptionsPanel = GameObject.Find("Options Panel");
            InitializeInstances(pongPrefab);
        }
        else if(scene.name == "Main Menu")
        {
            inGame = false;
            loadButton = GameObject.Find("Load").GetComponent<Button>();
            if (SaveSystem.SaveExists())
            {
                loadButton.interactable = true;
            }
            else
            {
                loadButton.interactable = false;
            }
        }
        else
        {
            inGame = false;
        }
    }

    private void LateUpdate()
    {
        if (inGame)
        {
            double maxPongScore = 5 + (double)(data.upgrades.Pong_Score_Limit.stacks * data.upgrades.Pong_Score_Limit.increaseValue);
            double maxPB = data.currentPB;
            double maxWins = data.currentWins;

            int maxBits = data.progress.numBits;
            int maxCores = data.progress.numCores;

            double maxNum = System.Math.Pow(2, maxBits);

            if (maxNum < maxPB || maxNum < maxWins || maxNum < maxPongScore || maxPongScore < 0 || maxPB < 0 || maxWins < 0)
            {
                if(data.prestigeLevel == 0)
                    LoadScene("Upgrade Process");
            }

            if(data.upgrades.Unlock_Music.stacks > 0)
            {
                AudioSource source = GetComponent<AudioSource>();
                if (source.isPlaying == false)
                {
                    source.loop = true;
                    source.clip = music;

                    source.Play();
                }
            }
        }
        else
        {
            if(SceneManager.GetActiveScene().name == "Main Menu")
            {
               
                if (SaveSystem.SaveExists())
                {
                    loadButton.interactable = true;
                }
                else
                {
                    loadButton.interactable = false;
                }
            }
        }
    }

    private void OnGUI()
    {
        if (inGame)
        {
            if (data.currentWins >= 1)
            {

                CurrentPBLabel.text = "CURRENT PB: " + data.currentPB;
                
                if (EnabledOptions == false)
                {
                    OptionsPanel.SetActive(true);
                    EnabledOptions = true;
                }
                
            }
            else
            {
                CurrentPBLabel.text = "";
                OptionsPanel.SetActive(false);
            }

            CurrentWinLabel.text = "WINS: " + data.currentWins;
        }

        if (alterMoney != 0)
        {
            data.currentPB = alterMoney;
            
        }
        if (alterWins != 0)
        {
            data.currentWins = alterWins;

        }
    }

    public void LoadGame()
    {
        GameData loadedData = SaveSystem.LoadData();
        if (loadedData != null)
        {
            data = loadedData;
            upgrades = data.upgrades;
            progress = data.progress;
        }
        else
        {
            data = new GameData(upgrades);
            SaveSystem.SaveData(data);
        }
    }

    public void NewGame()
    {
        if (SaveSystem.SaveExists())
        {
            if (!OverwritePrompt.activeInHierarchy)
            {
                OverwritePrompt.SetActive(true);
                return;
            }
        }

        //they clicked yes, or no save file. start a new game
        upgrades = new UpgradeData();
        progress = new ProgressData(); 
        data = new GameData(upgrades, progress);
        SaveSystem.SaveData(data);

        LoadScene("Pong Scene");
    }

    public void UpdateAudio()
    {
        audioMixer.SetFloat("MasterVol", settings.AudioMaster);
        audioMixer.SetFloat("MusicVol", settings.AudioMusic);
        audioMixer.SetFloat("SFXVol", settings.AudioSFX);
    }

    public void UpdateKeyMaps()
    {
        GameInputManager.SetKeyMap("Up", settings.upKey);
        GameInputManager.SetKeyMap("Down", settings.downKey);
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void AddPB(double value)
    {
        this.data.currentPB += value;
        this.data.totalPB += value;
        Debug.Log("current score: " + this.data.currentPB);
        SaveSystem.SaveData(data);
    }

    public void RemovePB(double value)
    {
        this.data.currentPB -= value;
        Debug.Log("current score: " + this.data.currentPB);
        SaveSystem.SaveData(data);
    }

    public void AddWin(double value)
    {
        this.data.currentWins += value;
        this.data.totalWins += value;
        Debug.Log("current wins: " + this.data.currentWins);
        SaveSystem.SaveData(data);
    }

    public GameData GetData()
    {
        return this.data;
    }

    public SettingsData GetSettings()
    {
        return this.settings;
    }

    public static GameManager GetManager()
    {
        return GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    public static void InitializeInstances(GameObject pongPrefab)
    {
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "Secret Upgrade")
            {
                GameManager.GetManager().InitializeSecretInstances();
                return;
            }
        }

        Upgrade theUpgrade = GameManager.GetManager().GetData().upgrades.Pong_Instance_Increase;

        int count = 0;
        GameObject instances = GameObject.Find("Instances");
        foreach (Transform t in instances.transform)
        {
            if (t.name.StartsWith("Pong"))
                count++;
        }
        for (int c = count; c <= theUpgrade.stacks; c++)
        {
            GameObject g = GameObject.Instantiate(pongPrefab, instances.transform);
            Vector3 pos = Vector3.zero;
            pos.x = 20 * c;
            g.transform.position = pos;

        }
    }

    public void InitializeSecretInstances()
    {
        GameObject pongPrefab = secretPrefab;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "Secret Upgrade") return;
        }

        Upgrade theUpgrade = GameManager.GetManager().GetData().upgrades.Pong_Instance_Increase;

        int count = 0;
        GameObject instances = GameObject.Find("SecretInstances");
        foreach (Transform t in instances.transform)
        {
            if (t.name.StartsWith("Secret"))
                count++;
        }
        for (int c = count; c <= theUpgrade.stacks; c++)
        {
            GameObject g = GameObject.Instantiate(pongPrefab, instances.transform);
            Vector3 pos = Vector3.zero;
            pos.x = 20 * c;
            g.transform.position = pos;

        }
    }

    public static void Prestige()
    {
        GameManager m = GameManager.GetManager();
        m.GetData().prestigeLevel++;
        SaveSystem.SaveData(m.GetData());
        LoadScene("Main Menu");
    }


}
