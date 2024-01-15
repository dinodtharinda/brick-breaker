using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBalls : MonoBehaviour
{

    public ObjectPool ballPool;

    void Start()
    {

        for (int i = 0; i <= 400; i++)
        {
            float randomX = Random.Range(-2f, 2f);
            float randomY = Random.Range(-2f, 2f);
            SpawnBall(new Vector2(randomX, -3f), Quaternion.identity);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


    void SpawnBall(Vector3 position, Quaternion rotation)
    {
        GameObject ball = ballPool.GetObjectFromPool(position, rotation);

        // Optionally, you can modify properties of the spawned ball
        // For example, changing its color
        SpriteRenderer ballRenderer = ball.GetComponent<SpriteRenderer>();
        if (ballRenderer != null)
        {
            ballRenderer.color = Color.red;
        }
    }
}
