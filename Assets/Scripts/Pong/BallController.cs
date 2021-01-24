using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public Vector2 velocityOverride;
    public float maxAngle = 75f;
    public float minAngle = 5f;

    bool inFocus = true;

    float BoundY = 5;
    float BoundX = 10;

    public float baseSpeed = 5f;

    GameManager manager;
    SpriteRenderer ballSprite;
    PongManager pManager; 

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        manager = GameManager.GetManager();
        spawnPoint = transform.parent.Find("Ball Spawn").transform.position;
        ballSprite = GetComponent<SpriteRenderer>();
        pManager = transform.parent.GetComponent<PongManager>();
        
    }

    private void LateUpdate()
    {
        Vector2 position = transform.localPosition;

        Vector3 pScale = transform.parent.localScale;

        if(velocityOverride != Vector2.zero)
        {
            rb2d.velocity = velocityOverride;
        }

        if (position.y > BoundY || position.y < - BoundY )
        {
            transform.localPosition = spawnPoint;
            rb2d.velocity /= 2f;
        }
        else
        {
            if (position.x > BoundX || position.x < -BoundX)
            {
                transform.localPosition = spawnPoint;
                rb2d.velocity /= 2f;
            }
        }

        float angle = Vector2.Angle(rb2d.velocity, rb2d.velocity.x >= 0 ? Vector2.right : Vector2.left );
        if (angle > maxAngle)
        {
            Vector2 nv = rb2d.velocity;
            nv.y += transform.localPosition.y > 0 ? -1.5f : 1.5f;
            nv.x += transform.localPosition.x > 0 ? -1.5f : 1.5f;
            rb2d.velocity = nv;
        }
        if (angle < minAngle && rb2d.velocity != Vector2.zero)
        {
            Vector2 nv = rb2d.velocity;
            nv.y += (transform.localPosition.y > 0 ? -1 : 1);
            rb2d.velocity = nv;
        }

        if(rb2d.velocity != Vector2.zero)
        {
            if(rb2d.velocity.magnitude < baseSpeed)
            {
                rb2d.velocity = rb2d.velocity.normalized * baseSpeed;
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
            //Invoke("GoBall", 1);
            return;
        }
        Upgrade ballSpeed = manager.GetData().upgrades.Ball_Speed;
        Upgrade ballValue = manager.GetData().upgrades.Ball_Value;

        currentValue = (ulong) Math.Pow(2,ballValue.stacks);

        int x = Random.Range(0, 2);
        Debug.Log("ball direction: " + x);
        x = x == 0 ? -1 : 1;
        var y = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(x, y);
        float rand = Random.Range(0, 2);

        if (ballSpeed.stacks > 0)
        {
            baseSpeed = 5 + ballSpeed.increaseValue * ballSpeed.stacks;
            rb2d.velocity = (dir * (baseSpeed));

        }
        else
        {
            baseSpeed = 5;
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
                //PongManager.Score(this, coll.collider.gameObject.name, type);
                RestartGame();
            }
        }

        if(type == BallTypes.RALLY)
        {
            Upgrade ballMultiplier = manager.GetData().upgrades.Ball_Multiplier;
            this.currentValue += ballMultiplier.GetRallyValue();
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
