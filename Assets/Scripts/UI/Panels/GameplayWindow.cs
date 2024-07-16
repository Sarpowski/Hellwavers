using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayWindow : MonoBehaviour
{
    [SerializeField] private HealthPanel healthPanel;
    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject EndGameScorePanel;
    public GameObject SuccesPanel;
    public void Initialize(int currentHealth)
    {
        healthPanel.Initialize(currentHealth);
        
        restartButton.SetActive(false);
        menuButton.SetActive(false);
        EndGameScorePanel.SetActive(false);
        SuccesPanel.SetActive(false);
    }

    public void UpdateHealthUIElements(int showHearthCount)
    {
        healthPanel.UpdateView(showHearthCount);
    }
    
    public void ShowGameOverScreen()
    {
        //TODO game Over Screen
        restartButton.SetActive(true);
        menuButton.SetActive(true);
        EndGameScorePanel.SetActive(true);
    }

    public void ShowGameSuccesScreem()
    {
        SuccesPanel.SetActive(true);
    }
}
