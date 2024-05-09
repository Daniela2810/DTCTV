using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemComand
{
    void Execute(GameObject target, InventoryObject itemType);
}
