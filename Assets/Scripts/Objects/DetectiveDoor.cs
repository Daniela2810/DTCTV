using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveDoor : MonoBehaviour, IInteractable
{
    private string LVName;
    private BoxCollider boxCollider; // Referencia al BoxCollider

    public delegate void InteractEvent();
    public static event InteractEvent OnInteract;

    private void Start()
    {
        LVName = null;
        boxCollider = GetComponent<BoxCollider>(); // Obtiene el componente BoxCollider
        boxCollider.enabled = false; // Desactiva el BoxCollider al inicio
        StartCoroutine(ActivateCollider()); // Inicia la corutina para activar el BoxCollider
        Cursor.visible = false;
    }

    private IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(7); // Espera 10 segundos
        boxCollider.enabled = true; // Activa el BoxCollider
    }

    public void Interact()
    {
        if (LVName != null)
        {
            SoundManager.instance.StopMusic();
            GameManager.instance.StartCase();
            SceneManager.instance.LoadScene(LVName);

            if (OnInteract != null)
            {
                OnInteract();
            }
        }
        else
        {
            Debug.Log("error");
        }
    }

    public void MisionLoad(string missionName)
    {
        LVName = missionName;
        SoundManager.instance.PlayMusic(1);
    }
}
