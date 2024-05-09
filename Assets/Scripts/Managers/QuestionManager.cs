using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    [Header("Objetos que lleven el script preguntas")]
    public List<Question> questions = new List<Question>();

    [Header("texto que se autocompletara con el de las preguntas")]
    public TextMeshProUGUI questionText;

    [Header("botones que se usaran para responder las preguntas")]
    public Button[] responseButtons;

    private List<Question> selectedQuestions = new List<Question>();
    private int currentQuestionIndex = 0;

    private void Start()
    {
        selectedQuestions.Clear();
        while (selectedQuestions.Count < 5)
        {
            Question randomQuestion = questions[Random.Range(0, questions.Count)];
            if (!selectedQuestions.Contains(randomQuestion))
            {
                selectedQuestions.Add(randomQuestion);
            }
        }

        ShowQuestion(selectedQuestions[0]);
    }

    private void ShowQuestion(Question question)
    {
        questionText.text = question.questionText;

        for (int i = 0; i < responseButtons.Length; i++)
        {
            responseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.options[i];
        }
    }

    public void CheckAnswer(int selectedOptionIndex)
    {
        Question currentQuestion = selectedQuestions[currentQuestionIndex];
        Button selectedButton = responseButtons[selectedOptionIndex-1];

        if (selectedOptionIndex == currentQuestion.correctOptionIndex)
        {
            selectedButton.image.color = Color.green;
            LevelManager.instance.AddPoints(750);
        }
        else
        {
            selectedButton.image.color = Color.red;
        }

        foreach (Button button in responseButtons)
        {
            button.interactable = false;
        }

        StartCoroutine(next());
    }

    IEnumerator next()
    {

        yield return new WaitForSeconds(2);
        foreach (Button button in responseButtons)
        {
            button.interactable = true;
            button.image.color = Color.white;
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < selectedQuestions.Count)
        {
            ShowQuestion(selectedQuestions[currentQuestionIndex]);
        }
        else
        {
            LevelManager.instance.QuestionCheck();
            foreach (Button button in responseButtons)
            {
                button.interactable = false;
            }
        }
    }
}
