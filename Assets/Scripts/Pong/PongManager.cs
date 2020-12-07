using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 + xPosition1, yPosition1, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + xPosition2, yPosition2, 100, 100), "" + PlayerScore2);

        GUI.Label(new Rect(Screen.width / 2, yPosition2, 100, 100), "" + manager.GetComponent<GameManager>().GetData().currentPB);

        if (gameEnded)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
            {
                PlayerScore1 = 0;
                PlayerScore2 = 0;
                theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
                gameEnded = false;
            }
        }

        if (PlayerScore1 == 5)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Computer WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            gameEnded = true;
        }
        else if (PlayerScore2 == 5)
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
                PlayerScore1++;
                manager.SendMessage("AddPB", 1);
            }
            else
            {
                PlayerScore2++;
                manager.SendMessage("AddPB", 2);
            }
            
    }
}
