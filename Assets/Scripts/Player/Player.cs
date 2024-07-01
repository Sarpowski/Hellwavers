using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour , IKillable , IDamageble<int>
{
    [SerializeField]  private CharacterMovementJoystick _movementJoystick;
    [SerializeField]  private PlayerHealthController   playerHealthController;
    [SerializeField]  private Animator _animator;
    [SerializeField] private Score _score;
    
    //bak buna
    public bool isDead { get; private set; } = false;
    public event Action<int> PlayerHealthChanged;
    public event Action PlayerDied;
    
    void Start()
    {    
        
        isDead = false;
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
        if (isDead == true)
        {
            Debug.Log("character is dead");
            return;
        } 
        // Movement logic
        _movementJoystick.Move();
    }
    private void PlayerHealthControllerOnHealthZero()
    {
        // TODO
        // durumlari ekle
        // controlleri birakcaz 
        Debug.Log("Event in Dead den haberi var");
        Debug.Log("isDEAD from method PlayerHealthControllerOnHealthZero() " + isDead);
        Die();
        
        //buraya karakter olunce health sisteminin pek onemli olmadigi ile ilgili bir sey yazmak lazim

    }
    
    private void PlayerHealthControllerOnTakeDamage(int currentHealth)
    {
        PlayerHealthChanged?.Invoke(currentHealth);
        
    }
   
    /*
    public void Die()
    {
        PlayerDied?.Invoke();
    }    
    */
    
    
    //bu fantastik methodu SOR
    
    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        Debug.Log("isDead"+ isDead);
       
        PlayerDied?.Invoke();
        
        if (_movementJoystick != null)
        {
            _movementJoystick.enabled = false;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
       
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
        }
        
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            
            collider.enabled = false;
            
        }
        
    //animator ekle setBool ile
     
        
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
