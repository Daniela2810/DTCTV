using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetectiveManager : MonoBehaviour
{
    public static DetectiveManager Instance { get; private set; }

    [Header("UI puntero")]
    public float lookDistance = 1f;
    public Image pointAim;
    public Image defaultPoint;

    [Header("UI de descripciones")]
    public GameObject descriptionCanvas;
    public TextMeshProUGUI descriptionText;

    [Header("script del detectivecamara")]
    public DetectiveCamera detectiveCamera;

    private Camera mainCamera;
    private RaycastHit hit;
    private GameObject currentObject;
    private bool isAnalyzing = false;
    private bool itemHolding = false;
    private Item selectedItem = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        pointAim = defaultPoint;
    }

    private void Update()
    {
        HandleRaycast();
    }

    private void HandleRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit, lookDistance))
        {
            ProcessHit();
        }
    }

    private void ProcessHit()
    {
        if (hit.transform.CompareTag("People") && Input.GetKeyDown(KeyCode.E))
        {
            NPCDialogue npcDialogue = hit.transform.GetComponent<NPCDialogue>();
            if (npcDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(npcDialogue);
            }
        }
        else if (hit.transform.CompareTag("Interactuable") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            IRequeriedObject interactableWithItem = hit.transform.GetComponent<IRequeriedObject>();
            if (itemHolding && selectedItem != null && interactableWithItem != null)
            {
                interactableWithItem.InteractWithInventoryItem(selectedItem.objectType);
            }
            else
            {
                IPickableObject inventoryObject = hit.transform.GetComponent<IPickableObject>();
                if (inventoryObject != null)
                {
                    inventoryObject.Pick();
                }
                else
                {
                    IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
        else if (hit.transform.CompareTag("Analyzable") && !isAnalyzing)
        {
            StartAnalysis(hit.transform.gameObject);
        }
    }

    private void StartAnalysis(GameObject obj)
    {
        currentObject = obj;
        isAnalyzing = true;
        SetAnalysisUI(true);
    }

    private void CompleteAnalysis()
    {
        descriptionText.text = currentObject.GetComponent<Description>().description;
        descriptionCanvas.SetActive(true);
        isAnalyzing = false;
        SetAnalysisUI(false);
    }

    public void SetItemCursor(Sprite newCursorSprite, Item item)
    {
        pointAim.sprite = newCursorSprite;
        pointAim.color = newCursorSprite == null ? new Color32(255, 255, 255, 0) : new Color32(255, 255, 255, 255);
        selectedItem = item;
        itemHolding = true;
    }

    public void SetDefaultCursor()
    {
        pointAim.sprite = defaultPoint.sprite;
        pointAim.color = defaultPoint.color;
        selectedItem = null;
        itemHolding = false;
    }

    private void SetAnalysisUI(bool isActive)
    {
        pointAim.gameObject.SetActive(!isActive);
    }

    public void LockCamera()
    {
        detectiveCamera.LockCamera();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnlockCamera()
    {
        detectiveCamera.UnlockCamera();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CinematicCamera()
    {
        detectiveCamera.LockCamera();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
