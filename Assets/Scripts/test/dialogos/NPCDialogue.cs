using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public List<DialogueNode> dialogueNodes;
    private int currentNodeIndex = 0;
    public int defaultDialogueIndex = 0;  // �ndice del di�logo por defecto

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
            ResetToDefaultDialogue();  // Restablece al di�logo por defecto si se pasa un �ndice fuera de rango
        }
    }

    public void ResetToDefaultDialogue()
    {
        currentNodeIndex = defaultDialogueIndex; // Establece el nodo de di�logo por defecto para la pr�xima interacci�n
    }
}
