using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOpenTurnOnAndOff : MonoBehaviour
{
    [Header("Objeto que contiene los objetos a desactivar")]
    public ObjectOpen objectOpen;

    [Header("lista de objetos a los cuales activar y desactivar colliders")]
    public List<Collider> ObjectInside = new List<Collider>();

    private bool open = false;

    private void Start()
    {
        objectOpen.OnObjectOpened += HandleObjectOpened;
        foreach (var collider in ObjectInside)
        {
            collider.enabled = false;
        }
    }

    private void HandleObjectOpened()
    {
        if (!open)
        {
            foreach (var collider in ObjectInside)
            {
                collider.enabled = true;
            }
            open = true;
        }
        else
        {
            foreach (var collider in ObjectInside)
            {
                collider.enabled = false;
            }
            open = false;
        }
    }

}
