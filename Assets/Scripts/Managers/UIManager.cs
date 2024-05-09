using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public TextMeshProUGUI[] PointTexts; // Un arreglo de TextMeshProUGUI
    public Transform targetPosition;

    private float moveDuration = 0.5f;
    private int currentIndex = 0; // Índice del punto de texto actual
    private Vector3 InitialPosition;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        // Desactiva todos los puntos de texto al inicio
        foreach (TextMeshProUGUI pointText in PointTexts)
        {
            pointText.gameObject.SetActive(false);
            InitialPosition = pointText.gameObject.transform.position;
        }
    }

    public void PointsAdder(int Points)
    {
        for (int i = 0; i < PointTexts.Length; i++)
        {
            int indexToUse = (currentIndex + i) % PointTexts.Length;
            TextMeshProUGUI currentPointText = PointTexts[indexToUse];

            if (!currentPointText.gameObject.activeSelf)
            {
                currentPointText.gameObject.SetActive(true);
                currentPointText.text = "+" + Points.ToString();

                StartCoroutine(MoveTextToPoint(currentPointText, targetPosition.position, moveDuration));
                break;
            }
        }
    }

    private IEnumerator MoveTextToPoint(TextMeshProUGUI pointText, Vector3 target, float duration)
    {
        yield return new WaitForSeconds(0.4f);
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            pointText.transform.position = Vector3.Lerp(pointText.transform.position, target, normalizedTime);
            yield return null;
        }

        pointText.transform.position = target;

        pointText.gameObject.SetActive(false);
        pointText.gameObject.transform.position = InitialPosition;
    }
}
