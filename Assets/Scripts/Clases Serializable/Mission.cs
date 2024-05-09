using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    [Header("nombre de la mision")]
    public string missionName;
    [Header("descripcion de la mision")]
    public string missionDescription;
    [Header("dificultad de la mision")]
    public Difficulty difficulty;
}
