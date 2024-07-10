using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageble<int>
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currentHealth = 3;
    private int _defaultHealth = 3;

    public event Action<int> HealthChanged; //ui icin

    //start
    private void Start()
    {
        _maxHealth = 3;
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
}