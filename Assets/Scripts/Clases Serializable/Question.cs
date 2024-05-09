using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Question : MonoBehaviour
{
    [Header("pregunta")]
    public string questionText;
    [Header("respuestas")]
    public List<string> options = new List<string>();
    [Header("respuesta correcta (recordar que el array empieza en 0)")]
    public int correctOptionIndex;
}
