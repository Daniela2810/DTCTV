using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mision : MonoBehaviour, IInteractable
{
    public string missionName;
    public string description;
    public string sceneName;

    public DetectiveDoor detectiveDoor;

    public void Interact()
    {
        detectiveDoor.MisionLoad(sceneName);
        Debug.Log(sceneName);
    }
}
