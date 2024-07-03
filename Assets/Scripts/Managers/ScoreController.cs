
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int killScore = 1;
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
            Projectile.killedAnEnemy += OnEnemyKilled;
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
        Projectile.killedAnEnemy -= OnEnemyKilled;
    }
    public void initScore()
    {   
        _score = 0;
        Debug.Log("begining score "+ _score);
    }

    public void add_score(int add)
    {
        _score = _score + 1;
        UpdateScoreUI();
    }
    private void OnEnemyKilled()
    {
        add_score(killScore);
    }
    private void UpdateScoreUI()
    {
        if (uiScreenText != null)
        {
            uiScreenText.text = " " + _score;
        }
    }
}
