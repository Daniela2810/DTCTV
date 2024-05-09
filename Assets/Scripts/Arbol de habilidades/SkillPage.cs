using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillPage", menuName = "Skill System/Skill Page")]
public class SkillPage : ScriptableObject
{
    public List<Sprite> icons; // Lista de iconos para cada habilidad en la ruta
    public DetectiveAbility skillType; // Tipo de habilidad para esta página
    [TextArea(3, 10)]
    public List<string> narrativeTexts; // Textos narrativos para cada nivel de habilidad en la ruta

    // Calcula el costo basado en el nivel actual de la habilidad
    public int GetSkillCost(int currentLevel)
    {
        return currentLevel+1; // Ejemplo simple: cada siguiente nivel cuesta su número de nivel
    }
}