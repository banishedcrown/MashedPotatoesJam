using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongManager : MonoBehaviour
{
    public static int PlayerScore = 0;
    public static int EnemyScore = 0;

    public int xPosition1 = -120;
    public int xPosition2 = 150;
    public int yPosition1 = 20;
    public int yPosition2 = 20;

    public GUISkin layout;

    GameObject theBall;
    static GameObject managerObject;
    GameManager manager;
    private bool gameEnded = false;

    GameObject pongScoreAI;
    GameObject pongScorePlayer;

    GameObject canvas;

    Text PlayerScoreText;
    Text EnemyScoreText;


    Upgrade scoreLimit;
    Upgrade autoRestart;

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        managerObject = GameObject.FindGameObjectWithTag("Manager");
        manager = managerObject.GetComponent<GameManager>();
        canvas = transform.Find("Canvas").gameObject;
        PlayerScoreText = canvas.transform.Find("PlayerScore").GetComponent<Text>();
        EnemyScoreText = canvas.transform.Find("EnemyScore").GetComponent<Text>();

        scoreLimit = manager.GetData().upgrades.Pong_Score_Limit;
        autoRestart = manager.GetData().upgrades.Pong_Auto_Resart;
    }

    void OnGUI()
    {
        GUI.skin = layout;
        PlayerScoreText.text = "" + PlayerScore;
        EnemyScoreText.text = "" + EnemyScore;

        if (gameEnded)
        {
            Time.timeScale = 0;
            if (autoRestart.stacks == 0)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 150, 50), "RESTART"))
                {
                    PlayerScore = 0;
                    EnemyScore = 0;
                    Time.timeScale = 1;
                    theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);                    
                    gameEnded = false;
                }
            }
            else
            {
                PlayerScore = 0;
                EnemyScore = 0;
                Time.timeScale = 1;
                theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
                gameEnded = false;
            }
        }
        else
        {
            
            if (PlayerScore == 5 + scoreLimit.stacks * scoreLimit.increaseValue)
            {
                GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Computer WINS");
                theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
                gameEnded = true;
            }
            else if (EnemyScore == 5 + scoreLimit.stacks * scoreLimit.increaseValue)
            {
                GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER WINS");

                if (!gameEnded) manager.AddWin(1);

                theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
                gameEnded = true;
            }
        }
        
    }

    public static void Score(BallController ball, string wallID, BallTypes type)
    {
        GameManager m = managerObject.GetComponent<GameManager>();
        Upgrade baseBall = m.GetData().upgrades.Ball_Value;
        if (wallID == "RightWall")
        {
            PlayerScore++;
            managerObject.SendMessage("AddPB", (long)(((long)ball.currentValue) *(1+baseBall.stacks*baseBall.increaseValue)));
        }
        else
        {
            EnemyScore++;
            managerObject.SendMessage("AddPB", (long)(((long)ball.currentValue) * (1 + baseBall.stacks * baseBall.increaseValue)));
        }
            
    }
}
