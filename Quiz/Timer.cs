using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeForQuestion = 30f;
    [SerializeField] float timeForCheck = 10f;
    float timerValue;
    public bool answerQuestion = false; // 문제 푸는 중 여부
    public float fillFraction; // 타이머 분수
    public bool loadNextQuestion; // 다음 문제 필요 여부
    public bool isCheckTimeOver = false; // 답 체크 타임 오버 여부

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(timerValue <= 0)
        {
            //문제 푸는 동안 TimeOver
            if(answerQuestion)   timerValue = timeForCheck;
            //답 체크 동안 TimeOver
            else
            {
                timerValue = timeForQuestion;
                loadNextQuestion = true;
                isCheckTimeOver = true; //true 변환
            } 

            answerQuestion = !answerQuestion;
        }
        else
        {
            isCheckTimeOver = false; //false로 변환
            if(answerQuestion)  fillFraction = timerValue / timeForQuestion;
            else    fillFraction = timerValue / timeForCheck;
        }
    }
}
