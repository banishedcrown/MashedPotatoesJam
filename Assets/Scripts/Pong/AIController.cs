﻿using System;
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
        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
        Vector2 Tpos = ball.transform.position;
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
        Upgrade size = manager.GetData().upgrades.PaddleSize;

        Vector3 scale = transform.localScale;

        scale.y = 1 + size.increaseValue * size.stacks;

    }

    GameObject getClosestBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        float minDistance = float.PositiveInfinity;
        Vector3 minPos = balls[0].transform.position;
        GameObject ball = balls[0];

        foreach(GameObject g in balls)
        {
            if (g.transform.position.x < 0)
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
