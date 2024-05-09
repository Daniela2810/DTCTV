using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelNotes : MonoBehaviour
{
    [Header("objeto que tendra el texto descriptivo")]
    public GameObject Note;
    [Header("texto que aparecera al posarse sobre el boton")]
    public TextMeshProUGUI noteText;

    private void Start()
    {
        Note.SetActive(false);
    }

    public void OnPointerEnter(Button button)
    {
        if (!button.interactable)
        {
            return;
        }

        Note.SetActive(true);

        string description = button.name;

        string TextContent = "";

        switch (description)
        {
            case "Nivel 1":
                TextContent = "Modo Fácil:\n" +
                                 "30 minutos para resolver 2 puzzles.";
                break;
            case "Nivel 2":
                TextContent = "No se detecta el DLC, por favor comprarlo para proseguir";
                break;
            case "Nivel 3":
                TextContent = "Modo Difícil:\n" +
                                 "15 minutos para resolver 4 puzzles.";
                break;
            case "Nivel 4":
                TextContent = "Modo Muy Difícil:\n" +
                                 "10 minutos para resolver 5 puzzles.";
                break;
        }

        noteText.text = TextContent;
    }

    public void OnPointerExit()
    {
        Note.SetActive(false);
    }
}
