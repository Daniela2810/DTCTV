using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public List<DialogueNode> dialogueNodes;
    private int currentNodeIndex = 0;
    public int defaultDialogueIndex = 0;  // Índice del diálogo por defecto

    public DialogueNode GetCurrentNode()
    {
        return dialogueNodes[currentNodeIndex];
    }

    public void AdvanceToNode(int nextNodeIndex)
    {
        if (nextNodeIndex < dialogueNodes.Count)
        {
            currentNodeIndex = nextNodeIndex;
        }
        else
        {
            ResetToDefaultDialogue();  // Restablece al diálogo por defecto si se pasa un índice fuera de rango
        }
    }

    public void ResetToDefaultDialogue()
    {
        currentNodeIndex = defaultDialogueIndex; // Establece el nodo de diálogo por defecto para la próxima interacción
    }
}
