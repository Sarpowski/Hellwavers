using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameplayWindow gameplayWindow;



    public ScoreController score_ui;
    //can be generic

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameplayWindow.Initialize(3);

        GameManager.Instance.player.PlayerHealthChanged += OnPlayerHealthChanged; //decreaseHealth
        GameManager.Instance.player.PlayerDied += OnPlayerDied; // player dead 
        EnemyManager.OnSuccess += OnSuccesEnd;
        //initial UI update 
        //UpdateHealthUI(GameManager.Instance.GetPlayerHealth());
    }

    private void OnSuccesEnd()
    {
        //TODO implement success screen
        Debug.Log("BOSS died and this sended from UI manager");
        OnPlayerWins();
    }

    private void OnDestroy()
    {
        Debug.Log("UI_PlayerHealth_Controller: Unsubscribing from events");
        GameManager.Instance.player.PlayerHealthChanged -= OnPlayerHealthChanged;
        GameManager.Instance.player.PlayerDied -= OnPlayerDied;
        EnemyManager.OnSuccess -= OnSuccesEnd;
    }

    private void OnPlayerHealthChanged(int currentHealth)
    {
        gameplayWindow.UpdateHealthUIElements(currentHealth);
    }

    private void OnPlayerDied()
    {
        gameplayWindow.ShowGameOverScreen();
    }

    private void OnPlayerWins()
    {
        gameplayWindow.ShowGameSuccesScreem();
    }
}