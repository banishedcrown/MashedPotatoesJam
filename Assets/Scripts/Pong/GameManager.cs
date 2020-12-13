using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameData data;
    UpgradeData upgrades;

    TMP_Text CurrentPBLabel;

    public GameObject OverwritePrompt;

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


    private void OnGUI()
    {
        if(inGame)
        {
            CurrentPBLabel.text = "CURRENT PB: " + data.currentPB;
            if (alterMoney != 0)
            {
                AddPB(alterMoney);
                alterMoney = 0;
            }
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
        data = new GameData(upgrades);
        SaveSystem.SaveData(data);
        LoadScene("Pong Scene");
    }

    public void LoadScene(string scene)
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
