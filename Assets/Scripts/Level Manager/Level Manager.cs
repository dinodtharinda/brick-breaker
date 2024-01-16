using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ObjectPool ballPool;
    [SerializeField] private GameObject[] livesImage;
    [SerializeField] private TextMeshProUGUI levelText;

    int currentSceneIndex;
    String currentSceneName;
    int lives = 5;


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
        levelText.text = currentSceneName.ToString();
    }


    void Update()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length <= 0)
        {
            Restart();

        }
    }

    void Restart()
    {
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            lives--;
            livesImage[lives].SetActive(false);
            ballPool.SpawnBall(new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }


}
