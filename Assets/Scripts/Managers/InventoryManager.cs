using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventorySlot
{
    public Image image;
    public Item item;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject InventoryHUD;
    public List<InventorySlot> inventorySpaces = new List<InventorySlot>();
    public Image ObjectImage;
    public Text objectName;
    public Text objectDescription;

    private List<Item> inventory = new List<Item>();
    private bool inventaryOpen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HUDinventory();
        }
    }

    public void AddItem(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }

        foreach (InventorySlot slot in inventorySpaces)
        {
            if (!slot.image.enabled)
            {
                slot.image.enabled = true;
                slot.image.sprite = item.objectIcon;
                slot.image.gameObject.SetActive(true);
                slot.item = item;
                break;
            }
        }
    }

    public Item GetItemByType(InventoryObject itemType)
    {
        foreach (Item item in inventory)
        {
            if (item.objectType == itemType)
            {
                return item;
            }
        }
        return null;
    }

    public void RemoveItem(Item item)
    {
        int index = -1;
        for (int i = 0; i < inventorySpaces.Count; i++)
        {
            if (inventorySpaces[i].item == item)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return;
        }
        inventory.Remove(item);

        for (int i = index; i < inventorySpaces.Count - 1; i++)
        {
            inventorySpaces[i].item = inventorySpaces[i + 1].item;
            inventorySpaces[i].image.sprite = inventorySpaces[i + 1].image.sprite;
            inventorySpaces[i].image.enabled = inventorySpaces[i + 1].image.enabled;
            inventorySpaces[i].image.gameObject.SetActive(inventorySpaces[i + 1].image.gameObject.activeSelf);
        }

        InventorySlot lastSlot = inventorySpaces[inventorySpaces.Count - 1];
        lastSlot.item = null;
        lastSlot.image.sprite = null;
        lastSlot.image.enabled = false;
        lastSlot.image.gameObject.SetActive(false);
    }

    public void RemoveItem(InventoryObject itemType)
    {
        Item item = GetItemByType(itemType);
        if (item != null)
        {
            RemoveItem(item);
        }
    }

    public void ItemRefresh(int slot)
    {
        if (inventorySpaces[slot] != null && inventorySpaces[slot].image.enabled)
        {
            ObjectImage.color = new Color32(255, 255, 255, 255);
            ObjectImage.sprite = inventorySpaces[slot].image.sprite;
            objectName.text = inventorySpaces[slot].item.objectName;
            objectDescription.text = inventorySpaces[slot].item.objectDescription;
        }
    }

    public void ItemRefreshOut(int slot)
    {
        ObjectImage.color = new Color32(255, 255, 255, 0);
        ObjectImage.sprite = null;
        objectName.text = null;
        objectDescription.text = null;
    }

    public void SelectItem(int slot)
    {
        if (inventorySpaces[slot] != null && inventorySpaces[slot].image.enabled)
        {
            Item selectedItem = inventorySpaces[slot].item;
            DetectiveManager.Instance.SetItemCursor(inventorySpaces[slot].image.sprite, selectedItem);
            HUDinventory();
        }
    }

    private void HUDinventory()
    {
        inventaryOpen = !inventaryOpen;
        InventoryHUD.SetActive(inventaryOpen);
        if (inventaryOpen)
            DetectiveManager.Instance.LockCamera();
        else
            DetectiveManager.Instance.UnlockCamera();
    }
}
