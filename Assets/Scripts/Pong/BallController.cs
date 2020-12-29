using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update

    public BallTypes type;

    public AudioClip score;
    public AudioClip PaddleBounce;
    public AudioClip EdgeBounce;

    private Rigidbody2D rb2d;
    private Vector3 spawnPoint;
    private AudioSource audioSource;

    public float currentValue = 1;

    public bool instancedBall = true;
    bool inFocus = true;

    float BoundY = 5;
    float BoundX = 10;

    GameManager manager;
    SpriteRenderer ballSprite;
    PongManager pManager; 

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        manager = GameManager.GetManager();
        spawnPoint = transform.parent.Find("Ball Spawn").transform.position;
        ballSprite = GetComponent<SpriteRenderer>();
        pManager = transform.parent.GetComponent<PongManager>();
        
        Invoke("GoBall", 2);
    }

    private void LateUpdate()
    {
        Vector2 position = transform.localPosition;

        Vector3 pScale = transform.parent.localScale;

        if (position.y > BoundY || position.y < - BoundY )
        {
            transform.position = spawnPoint;
        }
        else
        {
            if (position.x > BoundX || position.x < -BoundX)
            {
                transform.position = spawnPoint;
            }
        }

        if (transform.parent.localPosition == Vector3.zero)
        {
            inFocus = true;
        }
        else
        {
            inFocus = false;
        }
    }

    void GoBall()
    {

        if(pManager.gameEnded)
        {
            Invoke("GoBall", 1);
            return;
        }
        Upgrade ballSpeed = manager.GetData().upgrades.Ball_Speed;
        Upgrade ballValue = manager.GetData().upgrades.Ball_Value;

        currentValue = 1 + ballValue.stacks * ballValue.increaseValue;

        var x = Random.Range(0, 1);
        x = x == 0 ? -1 : 1;
        var y = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(x, y);
        float rand = Random.Range(0, 2);

        if (ballSpeed.stacks > 0)
        {
            rb2d.velocity = (dir * (5 + ballSpeed.increaseValue * ballSpeed.stacks));
        }
        else
        {
            rb2d.velocity = dir * 5;
        }

        rb2d.velocity *= transform.parent.localScale;
        if(inFocus) audioSource.PlayOneShot(PaddleBounce);
    }
    void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        transform.position = spawnPoint;

        type = GenerateType();
        if(type == BallTypes.RALLY)
        {
            ballSprite.color = Color.red;
        }
        else
        {
            ballSprite.color = Color.white;
        }
    }

    void RestartGame()
    {
        Upgrade setSpeed = manager.GetData().upgrades.Pong_Set_Speed;
        ResetBall();
        Invoke("GoBall", 1 *(1 - setSpeed.stacks*setSpeed.increaseValue));
        if (inFocus) audioSource.PlayOneShot(score);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;
            if (inFocus) audioSource.PlayOneShot(PaddleBounce);
            
        }
        else
        {
            if (coll.collider.CompareTag("Wall"))
            {

                if (inFocus) audioSource.PlayOneShot(EdgeBounce);
                
            }
            if (coll.collider.CompareTag("Goal"))
            {
                PongManager.Score(this, coll.collider.gameObject.name, type);
                RestartGame();
            }
        }

        if(type == BallTypes.RALLY)
        {
            Upgrade ballMultiplier = manager.GetData().upgrades.Ball_Multiplier;
            this.currentValue += (ballMultiplier.stacks * ballMultiplier.increaseValue);
        }
    }

    BallTypes GenerateType()
    {
        Upgrade ballRally = manager.GetData().upgrades.Ball_Rally;
        float rallyChance = ballRally.stacks * ballRally.increaseValue;
        float rand = Random.Range(0f, 1f);
        if (rand < rallyChance)
        {
            return BallTypes.RALLY;
        }


        return BallTypes.NORMAL;
    }
}
