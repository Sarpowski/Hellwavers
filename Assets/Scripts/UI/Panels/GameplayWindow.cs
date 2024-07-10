using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayWindow : MonoBehaviour
{
    [SerializeField] private HealthPanel healthPanel;
    public GameObject restartButton;
    public GameObject menuButton;

    public void Initialize(int currentHealth)
    {
        healthPanel.Initialize(currentHealth);
        
        
        restartButton.SetActive(false);
        menuButton.SetActive(false);
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
    }

    
}
