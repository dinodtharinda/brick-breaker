using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public int poolSize = 10;
    public int maxActiveObjects = 400;
    public int keepActiveBalls = 300;  // Number of balls to keep active
    public float deactivationProbability = 0.01f;

    private List<GameObject> pool;

    private void Start()
    {
        pool = new List<GameObject>(poolSize);
        InitializePool();
    }

    private void Update()
    {
        if (ShouldDeactivateRandomly())
        {
            DeactivateRandomObjects();
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = InstantiateObject(Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private GameObject InstantiateObject(Vector3 position, Quaternion rotation)
    {
        GameObject newObj = Instantiate(prefab, position, rotation);
        newObj.SetActive(false);
        return newObj;
    }

    private bool ShouldDeactivateRandomly()
    {
        return Random.value < deactivationProbability;
    }

    private void DeactivateRandomObjects()
    {
        int activeCount = GetActiveObjectCount();

        // Calculate the number of balls to deactivate
        int ballsToDeactivate = Mathf.Max(0, activeCount - keepActiveBalls);

        // Deactivate a portion of the balls randomly
        for (int i = 0; i < ballsToDeactivate; i++)
        {
            GameObject obj = GetRandomActiveObject();
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private GameObject GetRandomActiveObject()
    {
        List<GameObject> activeObjects = pool.FindAll(obj => obj.activeSelf);
        if (activeObjects.Count > 0)
        {
            return activeObjects[Random.Range(0, activeObjects.Count)];
        }
        return null;
    }

    private int GetActiveObjectCount()
    {
        int activeCount = 0;
        foreach (GameObject obj in pool)
        {
            if (obj.activeSelf)
            {
                activeCount++;
            }
        }
        return activeCount;
    }

    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        if (GetActiveObjectCount() < maxActiveObjects)
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                {
                    obj.transform.SetPositionAndRotation(position, rotation);
                    obj.SetActive(true);
                    return obj;
                }
            }

            GameObject newObj = InstantiateObject(position, rotation);
            pool.Add(newObj);
            return newObj;
        }
        else
        {
            return null;
        }
    }

    public void SpawnBall(Vector3 position, Quaternion rotation, Vector2 velocity)
    {
        try
        {
            GameObject ball = GetObjectFromPool(position, rotation);
            ball.GetComponent<Rigidbody2D>().velocity = velocity*2f;
            if (ball != null)
            {
                if (ball.TryGetComponent(out SpriteRenderer ballRenderer))
                {
                    ballRenderer.color = Color.white;
                }
                else
                {
                    // Debug.LogWarning("SpriteRenderer component not found on the ball object.");
                }
            }
            else
            {
                // Debug.LogWarning("Failed to get a ball from the pool.");
            }
        }
        catch (System.Exception e)
        {
            // Debug.LogWarning("Error: " + e.Message);
        }
    }
}
