using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour
{
    public Terminal[] terminals; // Arreglo de todas las terminales
    public GameObject door; // Referencia a la puerta que se abrirá

    public void CheckPuzzleCompletion()
    {
        Debug.Log("Checking if all terminals are connected...");
        foreach (Terminal terminal in terminals)
        {
            if (!terminal.isConnected)
            {
                Debug.Log("A terminal is still not connected.");
                return;
            }
        }
        Debug.Log("All terminals are connected. Opening door...");
        OpenDoor();
    }

    void OpenDoor()
    {
        Debug.Log("Puzzle completed, door opened!");
        door.SetActive(false); // Suponiendo que desactivar la puerta la "abre"
        Debug.Log("Puzzle completed, door opened!");
    }
}
