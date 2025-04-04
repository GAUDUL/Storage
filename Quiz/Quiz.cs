using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net.Sockets;
using UnityEngine.Assertions.Must;

public class Quiz : MonoBehaviour
{
    [Header("Question")] 
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestions;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnswerd;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Socring")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    public bool showEndSceen;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<Score>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnswerd = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        // 답변 x &  제한 시간 지남
        else if(!hasAnswerd && !timer.answerQuestion)
        {
            DisplayAnswers(-1);
            SetButtonState(false);
        }
        if(timer.isCheckTimeOver && isComplete)
        {
            showEndSceen = true;
        }

    }


    public void OnAnswerSelected(int index)
    {
        hasAnswerd = true;
        DisplayAnswers(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Scroe: " + scoreKeeper.CalculateScore()+ "%";
        if(progressBar.value == progressBar.maxValue)
            isComplete = true;
    }

    void DisplayAnswers(int index)
    {
        Image buttonImage;

        if(index == currentQuestions.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        } 
        else
        {
            correctAnswerIndex = currentQuestions.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestions.GetAnswer(correctAnswerIndex);
            questionText.text = $"The answer is '{correctAnswer}'";

            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if(questions.Count>0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestions();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestions = questions[index];

        if(questions.Contains(currentQuestions))
            questions.Remove(currentQuestions);
        
    }

    void DisplayQuestions()
    {
        questionText.text = currentQuestions.GetQuestion();

        for(int i = 0 ; i < 4; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestions.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0 ; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {

       for (int i = 0; i < answerButtons.Length; i++)
       {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
       }

    }
}
