using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ObjectPool ballPool;
    [SerializeField] private float speed;
    [SerializeField] private GameObject maxX;
    [SerializeField] private int giftCount = 0;


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

            // Calculate the new position of the paddle
            float newPaddleX = transform.position.x + (slideLength * speed);

            // Clamp the new position within a specific range
            float clampedX = Mathf.Clamp(newPaddleX, maxX.transform.position.x, -maxX.transform.position.x);

            // Update the paddle position
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

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

                        ballPool.SpawnBall(ball.transform.position, Quaternion.identity);
                        ballPool.SpawnBall(ball.transform.position, Quaternion.identity);

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
                    if (ball.activeInHierarchy && giftCount < 6)
                    {

                        ballPool.SpawnBall(ball.transform.position, Quaternion.identity);
                        ballPool.SpawnBall(ball.transform.position, Quaternion.identity);

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



}
