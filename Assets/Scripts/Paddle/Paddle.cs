using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float maxX = 7.5f;
    public ObjectPool ballPool;

    private int giftCount = 0;

    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private bool isDragging = false;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchStartPos.z = 0f;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            touchEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchEndPos.z = 0f;

            float slideLength = touchEndPos.x - touchStartPos.x;

            // Adjust the paddle position based on the slide length
            transform.position = new Vector3(transform.position.x + (slideLength * speed), transform.position.y, transform.position.z);

            // Update the start position for the next frame
            touchStartPos = touchEndPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gift"))
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            Debug.Log("Balls " + balls.Length);

            foreach (GameObject ball in balls)
            {
                if (ball != null && !ball.IsDestroyed())
                {
                    if (ball.activeInHierarchy && giftCount < 6 && balls.Length < 400)
                    {

                        SpawnBall(ball.transform.position, Quaternion.identity);
                        SpawnBall(ball.transform.position, Quaternion.identity);

                        Debug.Log("Gift Count " + giftCount);
                    }
                }
                else
                {
                    Debug.Log("Ball Not Active");
                }
            }
            giftCount++;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gift"))
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            Debug.Log("Balls " + balls.Length);

            foreach (GameObject ball in balls)
            {
                if (ball != null && !ball.IsDestroyed())
                {
                    if (ball.activeInHierarchy && giftCount < 6 )
                    {

                        SpawnBall(ball.transform.position, Quaternion.identity);
                        SpawnBall(ball.transform.position, Quaternion.identity);

                        Debug.Log("Gift Count " + giftCount);
                    }
                }
                else
                {
                    Debug.Log("Ball Not Active");
                }
            }
            giftCount++;
            Destroy(collision.gameObject);
        }
    }

    void SpawnBall(Vector3 position, Quaternion rotation)
    {
        try
        {
            GameObject ball = ballPool.GetObjectFromPool(position, rotation);

            if (ball != null)
            {
                // Use TryGetComponent instead of GetComponent to avoid redundant GetComponent calls
                if (ball.TryGetComponent(out SpriteRenderer ballRenderer))
                {
                    ballRenderer.color = Color.white;

                }
                else
                {
                    Debug.LogWarning("SpriteRenderer component not found on the ball object.");
                }
            }
            else
            {
                Debug.LogWarning("Failed to get a ball from the pool.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e.Message);
        }
    }


}