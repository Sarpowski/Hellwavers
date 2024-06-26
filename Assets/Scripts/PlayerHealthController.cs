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
    
    public event Action HealthZero;
    //start
    public void initHealth()
    {
        _maxHealth = setDefaultHealth(_maxHealth);
        _currentHealth = _maxHealth;
    }

    public int getHealth()
    {
        return _currentHealth;
    }
    
    
    private int setDefaultHealth(int currentMaxVal)
    {
        if (currentMaxVal> 0)
        {
            return currentMaxVal;
        }
        else
        {
            return _defaultHealth;
        }
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth < _minHealth) //fix minik bir logic bug var gibi
        {
            HealthZero?.Invoke();
            Debug.Log("dead");
        }
    }

    
}
