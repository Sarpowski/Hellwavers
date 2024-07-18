using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageble<int>
{
    [SerializeField] private int _maxHealth =10;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currentHealth = 3;
    private int _defaultHealth = 10;

    public event Action<int> HealthChanged; //ui icin

    //start
    private void Start()
    {
        
    }

    public void initHealth()
    {
        _maxHealth = SetDefaultHealth(_maxHealth);
        _currentHealth = _maxHealth;
    }

    public int getHealth()
    {
        return _currentHealth;
    }


    private int SetDefaultHealth(int currentMaxVal)
    {
        return currentMaxVal > 0 ? currentMaxVal : _defaultHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        HealthChanged?.Invoke(_currentHealth);
    }

    public void AddHealth(int healthAmount)
    {
        if (_currentHealth == 3)
        {
            return;
        }
        else
        {
            _currentHealth += healthAmount;
            HealthChanged?.Invoke(_currentHealth);

        }
    }
}