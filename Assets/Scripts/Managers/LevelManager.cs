using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("textode puntos")]
    public TMP_Text scoreText;

    [Header("Medallas")]
    public Image[] medals;

    [Header("puntaje Maximo necesario para la medallla de diamante")]
    public int maxPoints = 10000;

    public int currentPoints = 0;
    private int limitPoints = 0;
    private Coroutine currentAnimation;
    private float scoreAnimationDuration = 5.0f;

    public ValuePage valuePage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = "Puntaje: " + currentPoints.ToString();
        scoreText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            QuestionCheck();
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimatePoints(pointsToAdd));
    }

    private IEnumerator AnimatePoints(int pointsToAdd)
    {
        UIManager._instance.PointsAdder(pointsToAdd);
        pointsToAdd += limitPoints;
        limitPoints = pointsToAdd;
        int targetPoints = currentPoints + pointsToAdd;
        scoreText.gameObject.SetActive(true);
        scoreAnimationDuration = pointsToAdd * 1.7f;
        yield return StartCoroutine(AnimatePointsAnimation(pointsToAdd, targetPoints));
        limitPoints = 0;
        yield return new WaitForSeconds(2.0f);

        scoreText.gameObject.SetActive(false);
    }

    private IEnumerator AnimatePointsAnimation(int pointsToAdd, int targetPoints)
    {
        float timer = 0f;
        while (currentPoints <= targetPoints - 1)
        {
            float percentage = Mathf.Clamp01(timer / scoreAnimationDuration);
            int pointsIncrement = Mathf.RoundToInt(Mathf.Lerp(0, pointsToAdd, percentage));
            currentPoints += pointsIncrement;
            limitPoints -= pointsIncrement;

            scoreText.text = "Puntaje: " + currentPoints.ToString();

            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SubtractPointsAnimation(int pointsToSubtract, float duration)
    {
        float timer = 0f;
        int startPoints = currentPoints;
        int targetPoints = currentPoints - pointsToSubtract;

        while (currentPoints >= targetPoints)
        {
            float normalizedTime = Mathf.Clamp01(timer / 2000);
            int pointsDecrement = Mathf.RoundToInt(Mathf.Lerp(0, pointsToSubtract, normalizedTime));
            currentPoints -= pointsDecrement;

            currentPoints = Mathf.Max(currentPoints, targetPoints);

            scoreText.text = "Puntaje: " + currentPoints.ToString();

            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void QuestionCheck()
    {
        float finalPercentage = (float)currentPoints / maxPoints * 100;
        float multiplier = currentPoints / 8000.0f;
        float duration = currentPoints * multiplier;
        scoreText.gameObject.SetActive(true);
        WinOrLoseManager.Instance.Win();

       //if (finalPercentage > 0)
       //{
       //    StartCoroutine(FillMedal(medals[0], finalPercentage, 20, 0f));
       //}
       //if (finalPercentage > 20)
       //{
       //    StartCoroutine(FillMedal(medals[1], finalPercentage, 40, 2f));
       //    duration = 2;
       //    GameManager.instance.SkillPointIncrease(1);
       //}
       //if (finalPercentage > 40)
       //{
       //    StartCoroutine(FillMedal(medals[2], finalPercentage, 60, 4f));
       //    duration = 4;
       //    GameManager.instance.SkillPointIncrease(1);
       //}
       //if (finalPercentage > 60)
       //{
       //    StartCoroutine(FillMedal(medals[3], finalPercentage, 80, 6f));
       //    duration = 6;
       //    GameManager.instance.SkillPointIncrease(1);
       //}
       //if (finalPercentage > 80)
       //{
       //    StartCoroutine(FillMedal(medals[4], finalPercentage, 95, 8f));
       //    duration = 8;
       //    GameManager.instance.SkillPointIncrease(1);
       //}
       //if (finalPercentage > 95)
       //{
       //    duration = 10;
       //    GameManager.instance.SkillPointIncrease(1);
       //}

        valuePage.FinalCheck();
        StartCoroutine(SubtractPointsAnimation(currentPoints, duration));

    }

    public IEnumerator FillMedal(Image medal, float finalPercentage, float limitToFill, float delay)
    {
        yield return new WaitForSeconds(delay);

        float currentFillAmount = medal.fillAmount;
        float targetFillAmount = Mathf.Clamp(finalPercentage / limitToFill, 0.0f, 1.0f);

        float fillDuration = 2.0f;
        float startTime = Time.time;

        while (Time.time < startTime + fillDuration)
        {
            float normalizedTime = (Time.time - startTime) / fillDuration;
            medal.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, normalizedTime);
            yield return null;
        }
        medal.fillAmount = targetFillAmount;

        if (medal.fillAmount > 0.92f && medal.fillAmount != 1)
        {
            medal.fillAmount = 0.92f;
        }
        yield return new WaitForSeconds(5);
    }
}
