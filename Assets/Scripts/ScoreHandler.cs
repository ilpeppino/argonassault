using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{

    private int currentScore;
    private Text textScore;

    private void Awake()
    {
        currentScore = 0;
        textScore = GetComponent<Text>();
        textScore.text = currentScore.ToString();
    }


    public void OnEnemyDestroyed(int points)
    {
        currentScore += points;
        textScore.text = currentScore.ToString();
    }
}
