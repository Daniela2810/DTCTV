using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("lista de botones para diferentes acciones")]
    public List<ObjectCanvas> objectCanvas;

    public void exit()
    {
        for (int i = 0; i < objectCanvas.Count; i++)
        {
            objectCanvas[i].exitCanvas();
        }
    }
}
