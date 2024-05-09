using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class DialogueOption
{
    public string text;
    public int nextNode;
    public DetectiveAbility requiredSkill = DetectiveAbility.Charisma;
    public int requiredLevel = 0;  // Nivel necesario para que esta opción sea accesible
}

[System.Serializable]
public class DialogueNode
{
    public string text;
    public List<DialogueOption> options;

    public bool IsEndNode()
    {
        return options == null || options.Count == 0;
    }
}
