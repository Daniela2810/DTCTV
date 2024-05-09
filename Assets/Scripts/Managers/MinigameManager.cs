using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance { get; private set; }

    public LockpickController LockpickMinigame;

    public event System.Action<bool> LockpickMinigameComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void LockpickGame(Difficulty difficulty)
    {
        DetectiveManager.Instance.CinematicCamera();
        LockpickMinigame.gameObject.SetActive(true);
        LockpickMinigame.SetupGame(difficulty);
    }

    public void EndLockpickGame(bool result)
    {
        DetectiveManager.Instance.UnlockCamera();
        LockpickMinigameComplete?.Invoke(result);
        LockpickMinigame.gameObject.SetActive(false);
    }
}
