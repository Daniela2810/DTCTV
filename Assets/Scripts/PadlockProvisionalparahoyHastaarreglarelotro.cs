using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PadlockProvisionalparahoyHastaarreglarelotro : MonoBehaviour, IInteractable
{
    [Header("contraseña numerica")]
    public int[] correctPassword;

    [Header("objeto que funcionara al poner el codigo")]
    public ObjectMove doorToOpen;

    [Header("canvas de panel")]
    public Canvas buttonCanvas;

    [Header("texto numerico")]
    public TextMeshProUGUI displayText;

    public string newDescription { get; set; }

    public int maxDigits;
    public List<int> inputPassword = new List<int>();
    private DetectiveCamera detectiveCamera;
    private bool panelOpen = false; //hardcodeado
    private bool passwordTiling = false;

    public GameObject Key;

    private void Awake()
    {
        detectiveCamera = FindObjectOfType<DetectiveCamera>();
    }

    private void Start()
    {
        maxDigits = correctPassword.Length;
    }

    private void Update()
    {
        if (panelOpen)
        {
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString()) || Input.GetKeyDown(KeyCode.Keypad0 + i))
                {
                    int digit = i;
                    AddDigit(digit);
                }
            }
        }
    }

    public bool AddDigit(int digit)
    {
        if (!passwordTiling)
        {
            if (maxDigits == 0)
            {
                CheckPassword();
                return false;
            }

            maxDigits--;

            inputPassword.Add(digit);
            UpdateDisplayText();

            if (maxDigits == 0)
            {
                CheckPassword();
                return false;
            }

            return true;
        }
        else
            return false;
        
    }

    public void CheckPassword()
    {
        bool isCorrect = true;
        if (inputPassword.Count != correctPassword.Length)
        {
            isCorrect = false;
        }
        else
        {
            for (int i = 0; i < correctPassword.Length; i++)
            {
                if (inputPassword[i] != correctPassword[i])
                {
                    isCorrect = false;
                    break;
                }
            }
        }

        if (isCorrect)
        {
            doorToOpen.Interact();
            doorToOpen.gameObject.tag = "Interactuable";
            LevelManager.instance.AddPoints(700);
            buttonCanvas.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Key.gameObject.SetActive(true);
            Exit();
        }
        else
        {
            StartCoroutine(IncorrectPasswordAnimation());
        }
    }

    private IEnumerator IncorrectPasswordAnimation()
    {
        passwordTiling = true;

        for (int i = 0; i < 3; i++)
        {
            UpdateDisplayText();
            yield return new WaitForSeconds(0.5f);
            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        ResetInput();
        passwordTiling = false;
    }

    private void ResetInput()
    {
        inputPassword.Clear();
        maxDigits = correctPassword.Length;
        UpdateDisplayText();
    }

    private void UpdateDisplayText()
    {
        string display = "";

        foreach (int digit in inputPassword)
        {
            display += digit.ToString();
        }
        displayText.text = display;
    }


    public void Interact()
    {
        buttonCanvas.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        detectiveCamera.LockCamera();
        panelOpen = true;
    }

    public void Exit()
    {
        ResetInput();
        buttonCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        detectiveCamera.UnlockCamera();
        panelOpen = false;
    }
}