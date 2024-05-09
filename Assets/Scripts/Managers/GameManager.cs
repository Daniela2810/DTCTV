using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int money = 0;
    Difficulty difficulty;

    private bool levelStart;

    public bool LevelStart
    {
        get { return levelStart; }
    }

    public int ActualMoney
    {
        get { return money; }
    }

    public Difficulty actualdifficulty
    {
        get { return difficulty; }
    }

    private void Awake()
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

        Cursor.visible = false;
    }

    public void changedifficulty(Difficulty newdifficulty)
    {
        difficulty = newdifficulty;
    }

    public void StartCase()
    {
        levelStart = true;
    }

    public void EndCase()
    {
        levelStart = false;
    }

    public void ChangeMusic(int music)
    {
        SoundManager.instance.PlayMusic(music);
    }

    public void ChangeSound(int music)
    {
        SoundManager.instance.PlaySound(music, false);
    }
}
