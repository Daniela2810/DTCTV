using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    public Button[] optionButtons;

    private Coroutine typingCoroutine;
    private NPCDialogue currentDialogue;
    private string[] dialogueParts;
    public int currentPartIndex = 0;

    private bool dialogueInProgress = false;
    private bool isTextFullyDisplayed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(NPCDialogue npcDialogue)
    {
        if (!dialogueInProgress) // Esta condición debe permitir iniciar un diálogo si uno no está en curso
        {
            DetectiveManager.Instance.LockCamera();
            currentDialogue = npcDialogue;
            var node = npcDialogue.GetCurrentNode();
            dialogueParts = node.text.Split(';');
            dialogueCanvas.SetActive(true);
            InitializeButtons(false);
            DisplayNextPart();
            dialogueInProgress = true;
        }
        else
        {
            UserInputReceived(); // Revisa qué hace este método y si está interfiriendo con reiniciar el diálogo
        }
    }

    private void InitializeButtons(bool isActive)
    {
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    private void DisplayNextPart()
    {
        if (currentPartIndex < dialogueParts.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeSentence(dialogueParts[currentPartIndex].Trim()));
            currentPartIndex++;
        }
        else
        {
            ShowOptionsOrEndDialogue();
        }
    }

    private IEnumerator TypeSentence(string part)
    {
        isTextFullyDisplayed = false;
        dialogueText.text = "";

        foreach (char letter in part)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Delay for each letter
        }

        isTextFullyDisplayed = true;

        // Comprueba si aún quedan partes del diálogo para mostrar antes de llamar a ShowOptionsOrEndDialogue
        if (currentPartIndex < dialogueParts.Length)
        {
            yield return new WaitUntil(() => Input.anyKeyDown); // Espera a que el usuario presione una tecla
        }

        // Mueva la llamada a ShowOptionsOrEndDialogue a UserInputReceived para control manual
    }

    public void UserInputReceived()
    {
        if (isTextFullyDisplayed)
        {
            if (currentPartIndex >= dialogueParts.Length)
            {
                ShowOptionsOrEndDialogue(); // Llama aquí solo si todas las partes se han mostrado
            }
            else
            {
                DisplayNextPart(); // Continúa mostrando la siguiente parte si aún hay más
            }
        }
    }

    private void CompleteCurrentTextPart()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = dialogueParts[currentPartIndex - 1].Trim();

        if (currentPartIndex >= dialogueParts.Length)
        {
            ShowOptionsOrEndDialogue();
        }
    }

    private void UpdateDialogueOptions(List<DialogueOption> options, NPCDialogue npcDialogue)
    {
        InitializeButtons(false); // Asegúrate de que los botones estén desactivados antes de configurarlos

        int index = 0;
        foreach (var option in options)
        {
            if (index < optionButtons.Length && SkillManager.Instance != null && SkillManager.Instance.GetSkillLevel(option.requiredSkill) >= option.requiredLevel)
            {
                optionButtons[index].gameObject.SetActive(true);
                optionButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = option.text;
                optionButtons[index].onClick.RemoveAllListeners();
                optionButtons[index].onClick.AddListener(() =>
                {
                    InitializeButtons(false);  // Desactivar botones cuando una opción es seleccionada
                    npcDialogue.AdvanceToNode(option.nextNode);
                    DialogueNode nextNode = npcDialogue.GetCurrentNode();
                    if (nextNode != null)
                    {
                        currentDialogue = npcDialogue;
                        dialogueParts = nextNode.text.Split(';');
                        currentPartIndex = 0;
                        DisplayNextPart();
                    }
                    else
                    {
                        DesactivateDialogueMode(); // Finalizar diálogo si no hay más nodos
                    }
                });
            }
            index++;
        }
        for (; index < optionButtons.Length; index++)
        {
            optionButtons[index].gameObject.SetActive(false);
        }
    }

    private void ShowOptionsOrEndDialogue()
    {
        if (currentDialogue.GetCurrentNode().options.Count > 0 && currentPartIndex >= dialogueParts.Length)
        {
            InitializeButtons(true); // Activar botones solo si hay opciones disponibles
            UpdateDialogueOptions(currentDialogue.GetCurrentNode().options, currentDialogue);
        }
        else if (currentPartIndex >= dialogueParts.Length)
        {
            DesactivateDialogueMode();
            currentDialogue.ResetToDefaultDialogue();  // Restablece al diálogo por defecto
        }
    }

    public void DesactivateDialogueMode()
    {
        dialogueCanvas.SetActive(false);
        currentPartIndex = 0;
        InitializeButtons(false); // Desactivar todos los botones al finalizar el diálogo
        DetectiveManager.Instance.UnlockCamera();
        dialogueInProgress = false; // Restablece el estado para permitir nuevos diálogos
    }
}
