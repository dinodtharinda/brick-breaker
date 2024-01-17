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
        //    ballPool. SpawnBall(new Vector2(randomX, -3f), Quaternion.identity);
        }
    }
}
