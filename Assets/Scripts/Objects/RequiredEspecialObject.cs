using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredEspecialObject : MonoBehaviour, IRequeriedObject
{
    public List<InventoryObject> requiredItems;
    private List<IItemComand> objectLogics;

    private void Awake()
    {
        objectLogics = new List<IItemComand>(GetComponents<IItemComand>());
    }

    public void InteractWithInventoryItem(InventoryObject itemType)
    {
        if (requiredItems.Contains(itemType))
        {
            foreach (var logic in objectLogics)
            {
                logic.Execute(gameObject, itemType);
                DetectiveManager.Instance.SetDefaultCursor();
            }
        }
        else
        {
            DetectiveManager.Instance.SetDefaultCursor();
        }
    }
}
