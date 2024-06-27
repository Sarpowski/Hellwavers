using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour , IKillable , IDamageble<int>
{
    [SerializeField]  private CharacterMovementJoystick _movementJoystick;
    [SerializeField] private PlayerHealthController   playerHealthController;
    [SerializeField]private Animator _animator;
    
    public event Action<int> PlayerHealthChanged;
    public event Action PlayerDied;
    
    void Start()
    {    
        _movementJoystick = GetComponent<CharacterMovementJoystick>();
        playerHealthController = GetComponent<PlayerHealthController>();
       
        if (playerHealthController == null)
        {
            Debug.LogError("PlayerHealthController component is missing on the Player object.");
            return;
        }

        
        playerHealthController.initHealth();
        playerHealthController.DecreaseHealth += PlayerHealthControllerOnTakeDamage;
        playerHealthController.HealthZero += PlayerHealthControllerOnHealthZero;
    }

    private void PlayerHealthControllerOnHealthZero()
    {
        // TODO
        // durumlari ekle
        // controlleri birakcaz 
        Debug.Log("Event in Dead den haberi var");
        Die();
        
    }
    
    private void PlayerHealthControllerOnTakeDamage(int currentHealth)
    {
        PlayerHealthChanged?.Invoke(currentHealth);
        
    }
    
    public void Die()
    {
        PlayerDied?.Invoke();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H tusuna basildi");
            playerHealthController.TakeDamage(1);
            Debug.Log(playerHealthController.getHealth());
        }
        //damage burda handle edilcek fonksiyon ile
    }
    
    void FixedUpdate()
    {
        // Movement logic
        _movementJoystick.Move();
    }
    
    public void TakeDamage(int damageAmount)
    {
        playerHealthController.TakeDamage(damageAmount);   
    }

   
    public void Kill()
    {
        playerHealthController.TakeDamage(playerHealthController.getHealth());
    }

    
    
    
    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Taking damage");
            playerHealthController.TakeDamage(1);
 
        }
    }
}
