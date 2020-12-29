using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float speed = 3.0f;
    public float boundY = 4.25f;
    private Rigidbody2D rb2d;

    private float targetY = 0.0f;

    GameManager manager;

    GameObject parent;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        targetY = transform.position.y;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
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

        if(Math.Abs(Tpos.y - transform.position.y) < 0.05)
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
        Upgrade size = manager.GetData().upgrades.Player_Paddle_Size;

        Vector3 scale = transform.localScale;

        scale.y = 1 + size.increaseValue * size.stacks;

    }

    GameObject getClosestBall()
    {
        float minDistance = float.PositiveInfinity;
        Vector3 minPos = Vector3.zero;
        GameObject ball = null;
        Upgrade theUpgrade = manager.GetData().upgrades.AI_Enemy;

        foreach (Transform t in transform.parent)
        {
            if (t.CompareTag("Ball"))
            {
                GameObject g = t.gameObject;
                if (g.transform.localPosition.x < 0 - theUpgrade.stacks * theUpgrade.increaseValue)
                {
                    float dist = Vector2.Distance(transform.localPosition, g.transform.localPosition);
                    if (dist < minDistance)
                    {

                        minPos = g.transform.localPosition;
                        minDistance = dist;
                        ball = g;
                    }
                }
            }
        }

        return ball;
    }
}
