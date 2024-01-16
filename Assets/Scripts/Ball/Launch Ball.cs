using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public GameObject Paddle;
    [SerializeField] private float maxVelocity = 6f;

  public  bool isLaunch = false;
    private bool touchInProgress = false;

    private Vector3 clampMagnitude;


    private float increaseFactor;


    void Update()
    {

        if (isLaunch)
        {
            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
            }
            else
            {
                increaseFactor = 1.1f;
                rb.velocity *= increaseFactor;
            }
        }
        else
        {
            rb.transform.position = Paddle.transform.position + new Vector3(0, 0.1f, 0);
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    Launch();
                    isLaunch = true;
                    touchInProgress = false;
                }
            }
        }

    }

    void Launch()
    {

        rb.velocity = Vector2.up * 2f;
        clampMagnitude = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        angleBall(collision);
    }



    float angle;
    Vector2 newVelocity;
    Vector2 impactDirection;
    void angleBall(Collision2D collion)
    {
        impactDirection = (collion.GetContact(0).point - (Vector2)transform.position).normalized;
        angle = 5;
        newVelocity = Quaternion.Euler(0, 0, angle) * rb.velocity;
        rb.velocity = newVelocity;
    }
}
