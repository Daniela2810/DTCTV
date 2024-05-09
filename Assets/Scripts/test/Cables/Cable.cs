using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public colorValue cableColor;
    private Vector3 startPosition;
    private Vector3 dragOffset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        dragOffset = transform.position - GetMouseWorldPos();
        dragOffset.z = 0;
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + dragOffset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        Terminal terminal = other.GetComponent<Terminal>();
        if (terminal != null)
        {
            ConnectToTerminal(terminal);
        }
    }

    private void ConnectToTerminal(Terminal terminal)
    {
        if (terminal.requiredColor == cableColor && !terminal.isConnected) // Chequea que el cable y la terminal coincidan y que no esté ya conectado
        {
            transform.position = terminal.transform.position; // Alinea el cable con la terminal
            terminal.SetConnected(true); // Establece la terminal como conectada
            Debug.Log("Cable connected correctly!");
        }
        else
        {
            transform.position = startPosition; // Devuelve el cable a la posición inicial si no coincide
            Debug.Log("Incorrect terminal or already connected!");
        }
    }
}
