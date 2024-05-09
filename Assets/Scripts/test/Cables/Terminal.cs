using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public colorValue requiredColor;
    public bool isConnected = false;
    public TerminalController puzzleController; // Referencia al controlador del puzle

    public void SetConnected(bool connected)
    {
        isConnected = connected;
        if (isConnected)
        {
            puzzleController.CheckPuzzleCompletion(); // Verificar el estado completo del puzle
        }
    }
}
