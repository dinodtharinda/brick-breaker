using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
           if(balls.Length<=0){
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
           SceneManager.LoadScene(currentSceneIndex);
           }
    }
}
