using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float baseSpeed = 3.0f;
    private float speed = 0.0f;
    public float boundY = 4.25f;
    private Rigidbody2D rb2d;

    private float targetY = 0.0f;

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
        if (Input.GetKey(moveUp))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.y = -speed;
        }
        else
        {
            vel.y = 0;
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
        Upgrade size = upgrades.PaddleSize;
        Upgrade speed = upgrades.PlayerSpeed;
        
        
        Vector3 scale = transform.localScale;
        scale.y = 1 + size.increaseValue * size.stacks;
        transform.localScale = scale;

        this.speed = baseSpeed + speed.stacks*speed.increaseValue; 
    }

    /*public void OnDrag(PointerEventData data)
    {
        targetY = transform.position.y + data.delta.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = transform.position;
    }*/
}
