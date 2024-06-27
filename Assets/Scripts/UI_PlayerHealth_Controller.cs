using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerHealth_Controller : MonoBehaviour
{
    //can be list or array for dynamic 
     public GameObject heart_1;
     public GameObject heart_2;
     public GameObject heart_3;
    
     //can be generic
   
     private void Start()
     {
         Debug.Log("UI WORKS");
         GameManager.Instance.player.PlayerHealthChanged += OnPlayerHealthChanged; //decreaseHealth
         GameManager.Instance.player.PlayerDied += OnPlayerDied; //invoke etmeyi unutma
        //initial UI update 
        //UpdateHealthUI(GameManager.Instance.GetPlayerHealth());
        UpdateHealthUI(3);
     }

     private void OnDestroy()
     {
         Debug.Log("UI_PlayerHealth_Controller: Unsubscribing from events");
         GameManager.Instance.player.PlayerHealthChanged -= UpdateHealthUI;
         GameManager.Instance.player.PlayerDied -= OnPlayerDied;
     }

     
  
     private void OnPlayerHealthChanged(int currentHealth)
     {
         UpdateHealthUI(currentHealth);
     }
     
     private void OnPlayerDied()
     {
         ShowGameOverScreen();
     }
     
     private void UpdateHealthUI(int currentHealth)
     {
         Debug.Log("LOG FROM UI , Updating health UI to: " + currentHealth);
         switch (currentHealth)
         {
             case 3:
                 heart_1.SetActive(true);
                 heart_2.SetActive(true);
                 heart_3.SetActive(true);
                 break;
             case 2:
                 heart_1.SetActive(true);
                 heart_2.SetActive(true);
                 heart_3.SetActive(false);
                 break;
             case 1:
                 heart_1.SetActive(true);
                 heart_2.SetActive(false);
                 heart_3.SetActive(false);
                 break;
             case 0:
                 heart_1.SetActive(false);
                 heart_2.SetActive(false);
                 heart_3.SetActive(false);
                 break;
             default:
                 Debug.LogError("Invalid health value: " + currentHealth);
                 break;
         }
     }


     private void ShowGameOverScreen()
     {
         //TODO game Over Screen
         Debug.Log("Game Over");
     }
}
