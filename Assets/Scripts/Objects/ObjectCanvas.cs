using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCanvas : MonoBehaviour, IInteractable
{
    [Header("imagen del canvas")]
    public GameObject Image;

    [Header("boton de salida")]
    public Button exit;

    private DetectiveCamera detectiveCamera;

    public void Interact()
    {
        detectiveCamera = FindObjectOfType<DetectiveCamera>();
        Image.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        detectiveCamera.LockCamera();
    }

    public void exitCanvas()
    {
        detectiveCamera = FindObjectOfType<DetectiveCamera>();
        Image.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        detectiveCamera.UnlockCamera();
    }
}
