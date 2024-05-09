using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        if (sceneName == "TitleScreen" || sceneName == "Credits")
        {
            DetectiveManager.Instance.LockCamera();
        }
    }

    public void setDifficulty(Difficulty Difficulty)
    {
        GameManager.instance.changedifficulty(Difficulty);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        DetectiveManager.Instance.LockCamera();
    }
}
