using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public EnemyManager enemyManager;

    public event Action<bool> GameplayFinished;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;

            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
        StartGameplay();
    }

    private void Initialize()
    {
        SubscribeEvents();

        enemyManager.Initialize();
        player.Initialize();
    }

    private void StartGameplay()
    {
        enemyManager.StartGameplay();
        player.OnStartGameplay();
    }

    private void FinishGameplay(bool isSuccess)
    {
        enemyManager.FinishGameplay(isSuccess);
        player.OnFinishGameplay(isSuccess);

        GameplayFinished?.Invoke(isSuccess);
    }

    private void OnPlayerDied()
    {
        FinishGameplay(false);
    }

    private void OnFinalEnemyDied()
    {
        FinishGameplay(true);
    }

    private void SubscribeEvents()
    {
        UnsubscribeEvents();

        player.PlayerDied += OnPlayerDied;
        enemyManager.FinalEnemyDied += OnFinalEnemyDied;
    }

    private void UnsubscribeEvents()
    {
        player.PlayerDied -= OnPlayerDied;
        enemyManager.FinalEnemyDied -= OnFinalEnemyDied;
    }
}