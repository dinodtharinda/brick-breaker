using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ObjectPool ballPool;
    [SerializeField] private GameObject Paddle;
    [SerializeField] private GameObject Ball;

    [SerializeField] private GameObject[] livesImage;
    [SerializeField] private TextMeshProUGUI levelText;

    int currentSceneIndex;
    String currentSceneName;
    int lives = 5;


    void Awake()
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


    void ResetBallPosition()
    {
        Paddle.transform.position = new Vector3(0, Paddle.transform.position.y, Paddle.transform.position.z); 
         Vector3 paddlePosition = Paddle.transform.position;
        Instantiate(Ball, paddlePosition, Ball.transform.rotation);
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
            ResetBallPosition();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }


}
