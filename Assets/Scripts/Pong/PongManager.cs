using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PongManager : MonoBehaviour
{
    public int PlayerScore = 0;
    public int EnemyScore = 0;

    public int xPosition1 = -120;
    public int xPosition2 = 150;
    public int yPosition1 = 20;
    public int yPosition2 = 20;

    public GUISkin layout;

    GameObject theBall;
    static GameObject managerObject;
    GameManager manager;
    public bool gameEnded = false;

    GameObject pongScoreAI;
    GameObject pongScorePlayer;

    GameObject canvas;
    GameObject restartButton;

    Text PlayerScoreText;
    Text EnemyScoreText;


    Upgrade scoreLimit;
    Upgrade autoRestart;

    int scoreLimitAdjust = 0; 

    // Start is called before the first frame update
    void Start()
    {
        theBall = transform.Find("Ball").gameObject;
        managerObject = GameObject.FindGameObjectWithTag("Manager");
        manager = managerObject.GetComponent<GameManager>();
        canvas = transform.Find("Canvas").gameObject;
        restartButton = canvas.transform.Find("RestartButton").gameObject;
        restartButton.GetComponent<Button>().onClick.AddListener(this.Restart);
        PlayerScoreText = canvas.transform.Find("PlayerScore").GetComponent<Text>();
        EnemyScoreText = canvas.transform.Find("EnemyScore").GetComponent<Text>();

        scoreLimit = manager.GetData().upgrades.Pong_Score_Limit;
        autoRestart = manager.GetData().upgrades.Pong_Auto_Resart;
        
        Restart();
    }

    private void OnEnable()
    {
        PlayerScore = 0;
        EnemyScore = 0;
        if(theBall != null)
            theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        gameEnded = false;
    }

    void OnGUI()
    {
        GUI.skin = layout;
        PlayerScoreText.text = "" + PlayerScore;
        EnemyScoreText.text = "" + EnemyScore;

        if (gameEnded)
        {
            if (autoRestart.stacks == 0)
            {
                restartButton.SetActive(true);
            }
            else
            {
                PlayerScore = 0;
                EnemyScore = 0;
                theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
                gameEnded = false;
            }
        }
        else
        {
            float scoreCap = 5 + (scoreLimit.stacks + scoreLimitAdjust) * scoreLimit.increaseValue;
            if (PlayerScore >= (int)scoreCap)
            {
                GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Computer WINS");
                theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
                gameEnded = true;
            }
            else if (EnemyScore >= (int)scoreCap)
            {
                GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER WINS");

                if (!gameEnded) manager.AddWin(1);

                theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
                gameEnded = true;
            }
        }
        
    }

    public static void Score(BallController ball, string wallID, BallTypes type, GameObject PongManagerObject)
    {
        PongManager manager = PongManagerObject.GetComponent<PongManager>();
        manager.Score(ball, wallID, type);
    }

    public void Score(BallController ball, string wallID, BallTypes type)
    {
        GameManager m = managerObject.GetComponent<GameManager>();
        Upgrade baseBall = m.GetData().upgrades.Ball_Value;
        if (wallID == "RightWall")
        {
            PlayerScore++;
            managerObject.SendMessage("AddPB", (long)(((long)ball.currentValue) * (1 + baseBall.stacks * baseBall.increaseValue)));
        }
        else
        {
            EnemyScore++;
            managerObject.SendMessage("AddPB", (long)(((long)ball.currentValue) * (1 + baseBall.stacks * baseBall.increaseValue)));
        }

    }

    public void Restart()
    {
        PlayerScore = 0;
        EnemyScore = 0;
        if(theBall != null)
            theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        gameEnded = false;
        restartButton.SetActive(false);

    }

    public void changeScoreLimit(int dir)
    {
        scoreLimitAdjust += dir;
        if (scoreLimitAdjust > 0) scoreLimitAdjust = 0;
        if (scoreLimitAdjust < -scoreLimit.stacks) scoreLimitAdjust = -scoreLimit.stacks;

        Debug.Log("current limit: " + scoreLimitAdjust);
    }
}
