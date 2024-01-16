using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    int currentSceneIndex;
    String currentSceneName;
    public ObjectPool ballPool;
    public GameObject[] livesImage;
    int lives = 5;
    [SerializeField] private TextMeshProUGUI levelText;
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
        levelText.text = currentSceneName.ToString();
    }

    // Update is called once per frame
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
