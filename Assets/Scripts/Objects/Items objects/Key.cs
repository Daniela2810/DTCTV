using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IItemComand
{
    private ObjectOpen objectOpen;

    private void Awake()
    {
        objectOpen = GetComponent<ObjectOpen>();
    }

    public void Execute(GameObject target, InventoryObject itemType)
    {
        if (itemType == InventoryObject.BedroomKey)
        {
           objectOpen.Locked = false;
           objectOpen.Interact();
        }
    }
}
