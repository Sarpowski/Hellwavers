using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour , IDamageble<int>
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currentHealth;
    private int _defaultHealth = 100;
    
    public event Action  HealthZero;

    public event Action SetHealth; //ui icin

    public event Action<int> DecreaseHealth; //ui icin
   
    //start
    
    public void initHealth()
    {
        _maxHealth = setDefaultHealth(_maxHealth);
        _currentHealth = _maxHealth;
        SetHealth?.Invoke(); //send health info to UI
    }

    public int getHealth()
    {
        return _currentHealth;
    }
    
    
    private int setDefaultHealth(int currentMaxVal)
    {
       /* if (currentMaxVal> 0)
        {
            return currentMaxVal;
        }
        else
        {
            return _defaultHealth;
        }
        */
       return currentMaxVal > 0 ? currentMaxVal : _defaultHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (_currentHealth <= 0)
        {
            Debug.LogError("_currentHealth is not initialized.");
            //bisiler olmali  
            return;
        }
        
        _currentHealth -= damageAmount;
        DecreaseHealth?.Invoke(_currentHealth);
      
        
        if (_currentHealth <= _minHealth) //fix minik bir logic bug var gibi
        {
            HealthZero?.Invoke();
            // Debug.Log("dead");
          
        }
    }
    

    
}
