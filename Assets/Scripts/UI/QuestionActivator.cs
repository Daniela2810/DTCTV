using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionActivator : MonoBehaviour, IInteractable
{
    [Header("canvas de preguntas")]
    public GameObject questionUI;

    private DetectiveCamera detectiveCamera;

    private void Awake()
    {
        detectiveCamera = FindObjectOfType<DetectiveCamera>();
    }

    public void Interact()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        detectiveCamera.LockCamera();
        questionUI.SetActive(true);
    }
}
