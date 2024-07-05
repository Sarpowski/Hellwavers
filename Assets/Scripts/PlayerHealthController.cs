using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour , IDamageble<int>
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currentHealth=3; 
    private int _defaultHealth = 3;
    
    public event Action  HealthZero;

    public event Action SetHealth; //ui icin

    public event Action<int> DecreaseHealth; //ui icin
   
    //start
    private void Start()
    {
        _maxHealth = 3;
    }

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
       
       return currentMaxVal > 0 ? currentMaxVal : _defaultHealth;
    }

  
    public void TakeDamage(int damageAmount)
    {
      
       
        _currentHealth -= damageAmount;
        DecreaseHealth?.Invoke(_currentHealth);
      
        
        if (_currentHealth <= _minHealth) //fix minik bir logic bug var gibi
        {
            HealthZero?.Invoke();
            // Debug.Log("dead");
          
        }
    }
    

    
}
