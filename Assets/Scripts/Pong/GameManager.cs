using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameData data;
    UpgradeData upgrades;
    ProgressData progress;

    TMP_Text CurrentPBLabel;
    TMP_Text CurrentWinLabel;

    public GameObject OverwritePrompt;

    GameObject OptionsPanel;

    AudioClip music; 

    public int alterMoney = 0;
    bool inGame = false;
    private void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Manager");

        if (g.Length > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "Pong Scene")
        {
            inGame = true;
            CurrentPBLabel = GameObject.Find("CurrentPB").GetComponent<TMP_Text>();
            OptionsPanel = GameObject.Find("Options Panel");
            
        }
        else
        {
            inGame = false;
            Button button = GameObject.Find("Load").GetComponent<Button>();
            if (SaveSystem.SaveExists())
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Pong Scene")
        {
            inGame = true;
            CurrentPBLabel = GameObject.Find("CurrentPB").GetComponent<TMP_Text>();
            CurrentWinLabel = GameObject.Find("CurrentWins").GetComponent<TMP_Text>();
            OptionsPanel = GameObject.Find("Options Panel");
        }
        else
        {
            inGame = false;
            Button button = GameObject.Find("Load").GetComponent<Button>();
            if (SaveSystem.SaveExists())
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (inGame)
        {
            long maxPongScore = 5 + (long)(data.upgrades.Pong_Score_Limit.stacks * data.upgrades.Pong_Score_Limit.increaseValue);
            long maxPB = data.currentPB;
            long maxWins = data.currentWins;

            int maxBits = data.progress.numBits;
            int maxCores = data.progress.numCores;

            double maxNum = System.Math.Pow(2, maxBits);

            if (maxNum < maxPB || maxNum < maxWins || maxNum < maxPongScore)
            {
                LoadScene("Upgrade Process");
            }

            if(data.upgrades.Unlock_Music.stacks > 0)
            {
                AudioSource source = GetComponent<AudioSource>();
                source.loop = true;
                source.clip = music;
                source.Play();
            }
        }

    }

    private void OnGUI()
    {
        if (inGame)
        {
            if (data.currentWins > 5)
            {

                CurrentPBLabel.text = "CURRENT PB: " + data.currentPB;
                CurrentWinLabel.text = "WINS: " + data.currentWins;
                OptionsPanel.SetActive(true);

            }
            else
            {
                CurrentPBLabel.text = "";
                OptionsPanel.SetActive(false);
            }
        }

        if (alterMoney != 0)
        {
            AddPB(alterMoney);
            alterMoney = 0;
        }
    }

    public void LoadGame()
    {
        GameData loadedData = SaveSystem.LoadData();
        if (loadedData != null)
        {
            data = loadedData;
            upgrades = data.upgrades;
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

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void AddPB(long value)
    {
        this.data.currentPB += value;
        this.data.totalPB += value;
        Debug.Log("current score: " + this.data.currentPB);
        SaveSystem.SaveData(data);
    }

    public void RemovePB(long value)
    {
        this.data.currentPB -= value;
        Debug.Log("current score: " + this.data.currentPB);
        SaveSystem.SaveData(data);
    }

    public void AddWin(long value)
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

    public static GameManager GetManager()
    {
        return GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }
}
