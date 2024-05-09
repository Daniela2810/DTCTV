using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockpickController : MonoBehaviour
{
    public GameObject pick;
    public Transform startPosition;
    public Transform endPosition;
    public GameObject lockPrefab;
    public Difficulty currentDifficulty;

    private List<GameObject> locks = new List<GameObject>();
    private float moveSpeed;
    private int currentLockIndex;
    private bool gameIsActive = true;
    private KeyCode currentKeyToPress;
    private bool canAssignColor = true;
    private int lives = 3;

    void Start()
    {
        CanvasScaler canvasScaler = GetComponentInParent<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
        }
    }

    public void SetupGame(Difficulty difficulty)
    {
        ResetGame(); // Limpiar cualquier estado anterior

        float totalDistance = 1100f;
        float startOffset = 400f;
        int numLocks = 5;

        switch (difficulty)
        {
            case Difficulty.VeryEasy:
                numLocks = 3;
                moveSpeed = 300f;
                break;
            case Difficulty.Easy:
                numLocks = 4;
                moveSpeed = 400f;
                break;
            case Difficulty.Medium:
                numLocks = 5;
                moveSpeed = 500f;
                break;
            case Difficulty.Hard:
                numLocks = 6;
                moveSpeed = 600f;
                break;
            case Difficulty.VeryHard:
                numLocks = 7;
                moveSpeed = 700f;
                break;
            case Difficulty.Impossible:
                numLocks = 8;
                moveSpeed = 800f;
                break;
        }

        float usableDistance = totalDistance - startOffset;
        float spacing = usableDistance / (numLocks + 1);

        for (int i = 0; i < numLocks; i++)
        {
            float positionOffset = startOffset + spacing * (i + 1);
            Vector3 position = startPosition.position + (endPosition.position - startPosition.position).normalized * positionOffset;
            GameObject lockObj = Instantiate(lockPrefab, position, Quaternion.identity, startPosition);
            locks.Add(lockObj);
        }

        foreach (var item in locks)
        {
            Image image = item.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = true;
            }
        }

        gameIsActive = true;
        currentLockIndex = 0;
        canAssignColor = true;
        lives = 3;
        StartCoroutine(MovePick());
    }

    IEnumerator MovePick()
    {
        while (gameIsActive)
        {
            if (Vector3.Distance(pick.transform.position, endPosition.position) < 0.1f)
            {
                gameIsActive = false;
                Debug.Log("Minijuego completado con �xito.");
                ResetGame();
                MinigameManager.Instance.EndLockpickGame(true); // Llama al m�todo con `true` para indicar �xito
                yield break;
            }

            if (currentLockIndex < locks.Count && canAssignColor)
            {
                AssignRandomColorToLock(currentLockIndex);
                canAssignColor = false;
            }

            // Mover la ganz�a hacia la posici�n final
            pick.transform.position = Vector3.MoveTowards(pick.transform.position, endPosition.position, moveSpeed * Time.deltaTime);

            if (currentLockIndex < locks.Count && Vector3.Distance(pick.transform.position, locks[currentLockIndex].transform.position) < 100f)
            {
                gameIsActive = false;
                Debug.Log("Fallaste: la ganz�a toc� una cerradura sin abrir.");
                ResetGame();
                MinigameManager.Instance.EndLockpickGame(false); // Llama al m�todo con `false` para indicar fallo
                yield break;
            }

            if (currentLockIndex == locks.Count && Vector3.Distance(pick.transform.position, endPosition.position) > 100f)
            {
                moveSpeed += Vector3.Distance(pick.transform.position, endPosition.position); // Ajustar la velocidad para cubrir la distancia en 2 segundos
            }

            yield return null;
        }
    }

    void Update()
    {
        if (gameIsActive && currentLockIndex < locks.Count)
        {
            if (Input.GetKeyDown(currentKeyToPress))
            {
                Destroy(locks[currentLockIndex]); // Destruir la cerradura
                currentLockIndex++; // Avanzar al siguiente cierre
                canAssignColor = true; // Permitir la asignaci�n de un nuevo color para la siguiente cerradura
            }
            else if (Input.anyKeyDown && !Input.GetKeyDown(currentKeyToPress))
            {
                lives--;
                StartCoroutine(VibratePick()); // Vibrar la ganz�a como feedback visual
                if (lives <= 0)
                {
                    gameIsActive = false;
                    Debug.Log("Fallaste: No te quedan vidas.");
                    ResetGame();
                    MinigameManager.Instance.EndLockpickGame(false); // Llama al m�todo con `false` para indicar fallo
                }
            }
        }
    }

    void AssignRandomColorToLock(int lockIndex)
    {
        Color color = Color.white;
        switch (Random.Range(0, 4))
        {
            case 0:
                color = Color.blue;
                currentKeyToPress = KeyCode.W;
                break;
            case 1:
                color = Color.yellow;
                currentKeyToPress = KeyCode.A;
                break;
            case 2:
                color = Color.red;
                currentKeyToPress = KeyCode.S;
                break;
            case 3:
                color = Color.green;
                currentKeyToPress = KeyCode.D;
                break;
        }

        GameObject lockObject = locks[lockIndex];
        Renderer renderer = lockObject.GetComponent<Renderer>();
        Image image = lockObject.GetComponent<Image>();

        if (renderer != null)
        {
            renderer.material.color = color;
        }
        else if (image != null)
        {
            image.color = color;
        }
    }

    IEnumerator VibratePick()
    {
        Vector3 originalPosition = pick.transform.position;
        float time = 0.5f; // Duraci�n de la vibraci�n
        while (time > 0)
        {
            pick.transform.position = originalPosition + Random.insideUnitSphere * 10f; // Vibrar la ganz�a
            time -= Time.deltaTime;
            yield return null;
        }
        pick.transform.position = originalPosition; // Restablecer la posici�n original
    }

    private void ResetGame()
    {
        foreach (var lockObj in locks)
        {
            Destroy(lockObj);
        }

        locks.Clear();

        // Restablecer otros par�metros
        currentLockIndex = 0;
        canAssignColor = true;
        lives = 3;
        pick.transform.position = startPosition.transform.position;
    }
}