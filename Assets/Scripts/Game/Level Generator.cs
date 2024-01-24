using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public Button levelButtonPrefab; // Assign your button prefab in the inspector
    Button[] LevelButtons;

    private void Awake()
    {
        int ReachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

        // Assuming the first scene in the build settings is not a level scene
        LevelButtons = new Button[SceneManager.sceneCountInBuildSettings - 1];

        int buttonIndex = 0;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = SceneUtility.GetScenePathByBuildIndex(i);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);

            if (sceneName.StartsWith("Level - "))
            {
                int level = int.Parse(sceneName.Substring(8)); // Extract the level number

                LevelButtons[buttonIndex] = CreateLevelButton(sceneName, level);

                if (level > ReachedLevel)
                {
                    LevelButtons[buttonIndex].interactable = false;
                }

                buttonIndex++;
            }
        }
    }

    private Button CreateLevelButton(string sceneName, int level)
    {
        Button button = Instantiate(levelButtonPrefab, transform);
        button.name = "LevelButton" + level;

        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = level.ToString();
        }

        button.onClick.AddListener(() => LoadScene(sceneName)); // Use the scene name

        return button;
    }


    public void LoadScene(string sceneName)
    {
        PlayerPrefs.SetString("Level", sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("ReachedLevel", 1);
        string nextScene = "level - " + (currentLevel + 1);

        PlayerPrefs.SetInt("ReachedLevel", currentLevel + 1);
        SceneManager.LoadScene(nextScene);
    }
}
