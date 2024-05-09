using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    private int currentState = 0; // 0 = arriba, 1 = derecha, 2 = abajo, 3 = izquierda
    private bool isRotating = false;
    private Renderer renderer; // Referencia al Renderer para cambiar el color

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (!isRotating && KnobController.Instance.AreKnobsActive)
        {
            StartCoroutine(RotateKnob());
        }
    }

    IEnumerator RotateKnob()
    {
        isRotating = true;
        currentState = (currentState + 1) % 4; // Incrementa el estado
        for (float i = 0; i < 90; i += Time.deltaTime * 360)
        {
            transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 360)); // Rota 90 grados alrededor del eje Z en 1 segundo
            yield return null;
        }
        transform.localEulerAngles = new Vector3(0, 0, -90 * currentState); // Asegura que la rotación final es precisa.
        isRotating = false;
        KnobController.Instance.CheckSequence(); // Verifica la secuencia después de la rotación.
    }

    public int GetCurrentState()
    {
        return currentState;
    }

    public void SetColor(Color color)
    {
        renderer.material.color = color;
    }

    public void SetActive(bool isActive)
    {
        enabled = isActive; // Desactiva el componente MonoBehaviour para detener la interacción
    }
}
