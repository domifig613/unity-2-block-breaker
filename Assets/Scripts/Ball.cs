using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //config parms
    [SerializeField] PaddleScript paddle1;
    [SerializeField] bool hasStarted = false;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;

    //cached component references
    AudioSource myAudioSource;

    Rigidbody2D myRigidBodyody2d;

    // Use this for initialization
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBodyody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            xPush = UnityEngine.Random.Range(-2f, 2f);
           myRigidBodyody2d.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddleToBallVector + paddlePosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(0f, randomFactor), UnityEngine.Random.Range(0f, randomFactor));
        if (hasStarted)
        {
            slowDown();
            collisionWithPaddle(collision);
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);

            myRigidBodyody2d.velocity += velocityTweak;
        }
    }

    private void collisionWithPaddle(Collision2D collision)
    {
        if (collision.gameObject.name == paddle1.gameObject.name)
        {
            float paddleWidth = paddle1.GetComponent<Collider2D>().bounds.size.x;
        
            if (transform.position.x - paddle1.transform.position.x > paddleWidth/5 || transform.position.x - paddle1.transform.position.x < -paddleWidth / 5)
            {
                xPush = (transform.position.x - paddle1.transform.position.x) * randomFactor * 6;
            

                float position = GetComponent<Rigidbody2D>().velocity.x;
                if (xPush > 0 && position > 0 || position<0 && xPush < 0)
                {
                    xPush += position;
                }
                    GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    private void slowDown()
    {
        if (myRigidBodyody2d.velocity.y > yPush)
        {
            myRigidBodyody2d.velocity = new Vector2(myRigidBodyody2d.velocity.x, yPush);
        }
        if (myRigidBodyody2d.velocity.x > 10f || myRigidBodyody2d.velocity.x < -10f)
        {
            myRigidBodyody2d.velocity = new Vector2(myRigidBodyody2d.velocity.x*0.8f, yPush);
        }
    }
}
