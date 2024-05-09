using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpick : MonoBehaviour, IItemComand
{
    public Difficulty difficulty;

    private ObjectOpen objectOpen;

    private void Awake()
    {
        objectOpen = GetComponent<ObjectOpen>();
    }

    public void Execute(GameObject target, InventoryObject itemType)
    {
        if (itemType == InventoryObject.Lockpick)
        {
            MinigameManager.Instance.LockpickMinigameComplete += OnMinigameComplete;
            MinigameManager.Instance.LockpickGame(difficulty);
        }
    }

    private void OnMinigameComplete(bool result)
    {
        // Desuscribirse del evento para evitar múltiples llamadas
        MinigameManager.Instance.LockpickMinigameComplete -= OnMinigameComplete;

        if (result)
        {
            objectOpen.Locked = false;
            objectOpen.Interact();
            Debug.Log("lo lograste superar");
        }
    }
}
