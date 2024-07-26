using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private static int _score = 0;
    [SerializeField] public TMP_Text uiScreenText;

    void Start()
    {
        if (uiScreenText == null)
        {
            uiScreenText = GameObject.Find("Ui_score_text").GetComponent<TextMeshPro>();
        }

        if (uiScreenText != null)
        {
            initScore();
            EnemyAi.OnEnemyDeath += OnEnemyKilled;
            uiScreenText.text = " " + _score;
        }
        else
        {
            Debug.LogError("uiScreenText is not assigned also cant found :( ");
        }
    }

    private void OnDestroy()
    {
        EnemyAi.OnEnemyDeath -= OnEnemyKilled;
    }

    public void initScore()
    {
        _score = 0;
        Debug.Log("begining score " + _score);
    }

    private void AddScore(int score)
    {
        _score += score;
        UpdateScoreUI();
    }

    private void OnEnemyKilled(int score)
    {
        AddScore(score);
    }

    private void UpdateScoreUI()
    {
        if (uiScreenText != null)
        {
            uiScreenText.text = " " + _score;
        }
    }
}