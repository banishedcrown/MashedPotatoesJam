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
    static GameObject manager;
    private bool gameEnded = false;

    GameObject pongScoreAI;
    GameObject pongScorePlayer;

    GameObject canvas;

    Text PlayerScoreText;
    Text EnemyScoreText;

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        manager = GameObject.FindGameObjectWithTag("Manager");
        canvas = transform.Find("Canvas").gameObject;
        PlayerScoreText = canvas.transform.Find("PlayerScore").GetComponent<Text>();
        EnemyScoreText = canvas.transform.Find("EnemyScore").GetComponent<Text>();
    }

    void OnGUI()
    {
        GUI.skin = layout;
        PlayerScoreText.text = "" + PlayerScore;
        EnemyScoreText.text = "" + EnemyScore;

        if (gameEnded)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
            {
                PlayerScore = 0;
                EnemyScore = 0;
                theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
                gameEnded = false;
            }
        }

        if (PlayerScore == 5)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Computer WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            gameEnded = true;
        }
        else if (EnemyScore == 5)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            gameEnded = true;
        }

        
    }

    public static void Score(string wallID)
        {
        
            if (wallID == "RightWall")
            {
                PlayerScore++;
                manager.SendMessage("AddPB", 1);
            }
            else
            {
                EnemyScore++;
                manager.SendMessage("AddPB", 2);
            }
            
    }
}
