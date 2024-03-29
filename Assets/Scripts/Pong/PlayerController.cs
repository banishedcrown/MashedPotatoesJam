﻿using System;
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
    private float delayAI = 0f;

    private float startY;

    GameManager manager;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        targetY = transform.position.y;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var vel = Vector2.zero;

        if (useAI)
        {
            if (Time.time - delayAI > 0.5f)
                vel = doAI();
        }
        else
        {
            vel.y = 0;
        }

        if (GameInputManager.GetKey("Up"))
        {
            delayAI = Time.time;
            vel.y = speed;
        }
        else if (GameInputManager.GetKey("Down"))
        {
            delayAI = Time.time;
            vel.y = -speed;
        }

        rb2d.velocity = vel;

        var pos = transform.position;
        float Boundcheck = boundY * transform.parent.localScale.y;
        //pos.y = targetY;
        if (pos.y > startY + Boundcheck)
        {
            pos.y = startY + Boundcheck;
        }
        else if (pos.y < startY - Boundcheck)
        {
            pos.y = startY - Boundcheck;
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
        float minDistance = float.PositiveInfinity;
        Vector3 minPos = Vector3.zero;
        GameObject ball = null;
        Upgrade theUpgrade = manager.GetData().upgrades.AI_Player;

        foreach (Transform t in transform.parent)
        {
            if (t.CompareTag("Ball")) { 
                GameObject g = t.gameObject;
                if (g.transform.localPosition.x > 0 - theUpgrade.stacks * theUpgrade.increaseValue)
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
