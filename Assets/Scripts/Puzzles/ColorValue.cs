using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorValue : MonoBehaviour, IInteractable
{
    [Header("Script con la contraseña de colores")]
    public ColorPassword colorPassword;

    [Header("objeto que se movera al poner la contraseña correcta")]
    public ObjectMove objectMove;

    [Header("tipo de color")]
    public colorValue colorValue;

    private bool activated=false;
    public bool Activated
    {
        get { return activated; }
        set { activated = value; }
    }

    public void Interact()
    {
        activated = true;
        colorPassword.AddColor(colorValue, this);
        move();

    }
    public void move()
    {
        if (activated)
        {
            objectMove.Interact();
        }
    }
}
