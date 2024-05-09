using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manilla : MonoBehaviour, IInteractable
{
    public GameObject Lavado;

    public bool open;

    public void Interact()
    {
        if (open)
        {
            Lavado.gameObject.tag= "Untagged";
            open = false;
        }
        else
        {
            Lavado.gameObject.tag = "Interactuable";
            open = true;
        }
    }
}
