using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    private Vector3 spawnPoint;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spawnPoint = transform.parent.Find("Court").Find("Ball Spawn").transform.position;
        Invoke("GoBall", 2);
    }

    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(200, -150));
        }
        else
        {
            rb2d.AddForce(new Vector2(-200, -150));
        }
    }
    void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        transform.position = spawnPoint;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;
        }
    }
}
