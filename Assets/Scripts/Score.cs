using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int EarnedScore;

    void Start()
    {
        initScore();
        EnemyAi.OnEnemyDeath += OnEnemyKilled;
    }

    private void OnDestroy()
    {
        EnemyAi.OnEnemyDeath -= OnEnemyKilled;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void initScore()
    {
        EarnedScore = 0;
        // Debug.Log("begining score "+ EarnedScore);
    }

    private void OnEnemyKilled(int score)
    {
        AddScore(score);
    }
    
    private void AddScore(int points)
    {
        EarnedScore += points;
        // Debug.Log($"Score Updated: {EarnedScore}");
    }
    
}
