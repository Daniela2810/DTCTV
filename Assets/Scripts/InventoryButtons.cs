using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtons : MonoBehaviour
{
    public Image Items;
    public Image Star;
    public Image Hints;
    public Image options;

    public void newPage(GameObject image)
    {
        Items.gameObject.SetActive(false);
        Star.gameObject.SetActive(false);
        Hints.gameObject.SetActive(false);
        options.gameObject.SetActive(false);

        image.gameObject.SetActive(true);
    }
}
