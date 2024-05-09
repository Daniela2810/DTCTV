using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public GameObject timerBackground;

    public float currentTime = 0.0f;
    private float easyDuration = 900.0f;
    private float mediumDuration = 600.0f;
    private float hardDuration = 300.0f;


    private float abilityTime;

    private static Timer _instance;
    public static Timer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Timer>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GameManager.instance.LevelStart)
        {
            StartLevel();
        }
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            EndLevel();
        }

        TimerUI();
    }

    public void StartLevel()
    {
        SetDifficulty(Difficulty.Easy);
        timerText.gameObject.SetActive(true);
        timerBackground.gameObject.SetActive(true);
    }

    public void EndLevel()
    {
        currentTime = 0;
        timerText.gameObject.SetActive(false);
        timerBackground.gameObject.SetActive(false);
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                currentTime = easyDuration;
                break;
            case Difficulty.Medium:
                currentTime = mediumDuration;
                break;
            case Difficulty.Hard:
                currentTime = hardDuration;
                break;
            default:
                break;
        }

        currentTime += abilityTime;

        TimerUI();
    }

    private void TimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddAbilityTime(float timeToAdd)
    {
        abilityTime = 1800 * (timeToAdd);
    }
}

