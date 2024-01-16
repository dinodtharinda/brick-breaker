using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;  // The prefab to be pooled
    public int poolSize = 10;  // The initial size of the object pool
    public int maxActiveObjects = 400;  // Maximum number of active objects

    private List<GameObject> pool;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        pool = new List<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");


        if (balls.Length < maxActiveObjects)
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }

            // If no inactive objects are found, create a new one
            GameObject newObj = Instantiate(prefab, position, rotation);
            pool.Add(newObj);
            return newObj;
        }
        else
        {

            // If the active count reaches the maximum, return null or handle it as needed
            return null;
        }
    }



    public void SpawnBall(Vector3 position, Quaternion rotation)
    {
        try
        {
            GameObject ball = GetObjectFromPool(position, rotation);

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
