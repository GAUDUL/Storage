using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    Score scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<Score>();
    }


    public void ShowScore()
    {
        scoreText.text = "Congratulations!\nYou got a score of "
                        + scoreKeeper.CalculateScore() + "%";
    }
}
