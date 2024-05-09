using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPickableObject
{
    public InventoryObject objectType;
    public Sprite objectIcon;
    public string objectName;
    public string objectDescription;
    public bool isConsumable;
    public int quantity;
    public IItemComand itemCommand;  // Añadido para almacenar el comportamiento

    public override bool Equals(object obj)
    {
        if (obj is Item otherItem)
        {
            return objectType == otherItem.objectType;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return objectType.GetHashCode();
    }

    public void Pick()
    {
        InventoryManager.Instance.AddItem(this);
        gameObject.SetActive(false);
    }

    public void Use()
    {
        if (isConsumable)
        {
            quantity--;
            if (quantity <= 0)
            {
                InventoryManager.Instance.RemoveItem(this);
            }
        }
        else
        {
            InventoryManager.Instance.RemoveItem(this);
        }
    }
}

