using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip score;
    public AudioClip PaddleBounce;
    public AudioClip EdgeBounce;

    private Rigidbody2D rb2d;
    private Vector3 spawnPoint;
    private AudioSource audioSource;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spawnPoint = transform.parent.Find("Ball Spawn").transform.position;
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
        audioSource.PlayOneShot(PaddleBounce);
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
        audioSource.PlayOneShot(score);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;
            audioSource.PlayOneShot(PaddleBounce);
            
        }
        else
        {
            if (coll.collider.CompareTag("Wall"))
            {
                audioSource.PlayOneShot(EdgeBounce);
            }
        }
    }
}
