using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float baseSpeed = 3.0f;
    private float speed = 0.0f;
    public float boundY = 4.25f;
    private Rigidbody2D rb2d;

    private float targetY = 0.0f;

    private bool useAI = false;

    GameManager manager;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        targetY = transform.position.y;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var vel = rb2d.velocity;

        if (useAI)
        {
            vel = doAI();
        }
        else
        {
            vel.y = 0;
        }

        if (Input.GetKey(moveUp))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.y = -speed;
        }

        rb2d.velocity = vel;

        var pos = transform.position;
        //pos.y = targetY;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        UpgradeData upgrades = manager.GetData().upgrades;
        Upgrade size = upgrades.Player_Paddle_Size;
        Upgrade speed = upgrades.Player_Speed;
        Upgrade PlayerAI = upgrades.AI_Player;
        
        
        Vector3 scale = transform.localScale;
        scale.y = 1 + size.increaseValue * size.stacks;
        transform.localScale = scale;

        this.speed = baseSpeed + speed.stacks*speed.increaseValue;

        this.useAI = PlayerAI.stacks >= 1 ? true : false;
    }

    /*public void OnDrag(PointerEventData data)
    {
        targetY = transform.position.y + data.delta.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = transform.position;
    }*/

    Vector2 doAI()
    {
        var vel = rb2d.velocity;
        GameObject ball = getClosestBall();
        Vector2 Tpos;
        if (ball != null)
        {
            Tpos = ball.transform.position;
        }
        else
        {
            Tpos = transform.position;
        }

        if (Math.Abs(Tpos.y - transform.position.y) < 0.05)
        {
            vel.y = 0;
        }
        else if (Tpos.y > transform.position.y)
        {
            vel.y = speed;
        }
        else if (Tpos.y < transform.position.y)
        {
            vel.y = -speed;
        }

        return vel;
    }
    GameObject getClosestBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        float minDistance = float.PositiveInfinity;
        Vector3 minPos = Vector3.zero;
        GameObject ball = null;
        Upgrade theUpgrade = manager.GetData().upgrades.AI_Player;

        foreach (GameObject g in balls)
        {
            if (g.transform.position.x > 0 + theUpgrade.stacks * theUpgrade.increaseValue)
            {
                float dist = Vector2.Distance(transform.position, g.transform.position);
                if (dist < minDistance)
                {

                    minPos = g.transform.position;
                    minDistance = dist;
                    ball = g;
                }
            }
        }

        return ball;
    }
}
