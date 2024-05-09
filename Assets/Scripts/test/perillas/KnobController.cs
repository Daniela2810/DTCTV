using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobController : MonoBehaviour
{
    public static KnobController Instance;
    public Knob[] knobs;
    private int[] correctSequence = { 1, 1, 1, 1, 1 };
    public bool AreKnobsActive = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckSequence()
    {
        for (int i = 0; i < knobs.Length; i++)
        {
            if (knobs[i].GetCurrentState() != correctSequence[i])
            {
                Debug.Log("Perilla " + (i + 1) + " está incorrecta. Estado actual: " + knobs[i].GetCurrentState());
                return; // Si alguna perilla no está correcta, termina la verificación
            }
        }

        Debug.Log("Secuencia correcta! Acción desbloqueada.");
        foreach (Knob knob in knobs)
        {
            knob.SetColor(Color.blue); // Cambia el color a azul
            knob.SetActive(false); // Desactiva la interacción
        }
        AreKnobsActive = false; // Desactiva todas las perillas
    }
}
