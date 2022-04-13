using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    void Start()
    {
            scoreText.text = $"Score: {score}";
    }
    public void addScore(int add)
    {
        score += add;
        scoreText.text = $"Score: {score}";
    }
    public void resetScore()
    {
        score = 0;
        scoreText.text = $"Score: {score}";
    }
}
