using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score = 0;

    public event Action<int> OnScoreChanged;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int points) {
        score += points;
        OnScoreChanged?.Invoke(score);  
    }

    public void resetScore() {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
}
