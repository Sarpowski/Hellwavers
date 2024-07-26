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

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameplayWindow.Initialize(3);

        GameManager.Instance.player.PlayerHealthChanged += OnPlayerHealthChanged; //decreaseHealth
        GameManager.Instance.GameplayFinished += OnGameplayFinished; // player dead 

        //initial UI update 
        //UpdateHealthUI(GameManager.Instance.GetPlayerHealth());
    }

    private void OnDestroy()
    {
        Debug.Log("UI_PlayerHealth_Controller: Unsubscribing from events");
        GameManager.Instance.player.PlayerHealthChanged -= OnPlayerHealthChanged;
        GameManager.Instance.GameplayFinished -= OnGameplayFinished;
    }

    private void OnPlayerHealthChanged(int currentHealth)
    {
        gameplayWindow.UpdateHealthUIElements(currentHealth);
    }

    private void OnGameplayFinished(bool isSuccess)
    {
        if (isSuccess)
        {
            gameplayWindow.ShowGameSuccesScreem();
        }
        else
        {
            gameplayWindow.ShowGameOverScreen();
        }
    }
}