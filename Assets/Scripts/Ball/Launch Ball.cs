using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxVelocity = 6f;
    bool isLaunch = false;

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
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Launch();
                    isLaunch = true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Launch();
                isLaunch = true;
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
