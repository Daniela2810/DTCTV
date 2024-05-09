using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineInteract : MonoBehaviour
{
    private RaycastHit hitObject;
    private Transform hightlight;
    private DetectiveManager detectiveManager;

    private void Awake()
    {
        detectiveManager = FindObjectOfType<DetectiveManager>();
    }

    void Update()
    {

        if (hightlight != null)
        {
            hightlight.gameObject.GetComponent<Outline>().enabled = false;
            hightlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hitObject, detectiveManager.lookDistance))
        {
            hightlight = hitObject.transform;

            if (hightlight.CompareTag("Interactuable"))
            {
                if (hightlight.gameObject.GetComponent<Outline>() != null)
                {
                    hightlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = hightlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    hightlight.gameObject.GetComponent<Outline>().OutlineColor = Color.yellow;
                    hightlight.gameObject.GetComponent<Outline>().OutlineWidth = 8f;
                }
            }
            else
            {
                hightlight = null;
            }
        }
    }
}
