using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour, IKillable, IDamageble<int>
{
    [SerializeField] private CharacterMovementJoystick _movementJoystick;
    [SerializeField] private PlayerHealthController playerHealthController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Score _score;
    [SerializeField] private DetectTarget _detectTarget;
    
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
            Debug.LogError("PlayerHealthController component  missing on the Player.");
            return;
        }


        playerHealthController.initHealth();
        playerHealthController.HealthChanged += OnPlayerHealthChanged;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H button is pressed");
            playerHealthController.TakeDamage(1);

            Debug.Log(playerHealthController.getHealth());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space button is pressed");
            _movementJoystick.Dash();
        }
        //test
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

    private void BeforeDieMusic()
    {
     //   AudioManager.Instance.PlayMusic("TEST_DIE");
    }

    private void OnPlayerHealthChanged(int currentHealth)
    {
        PlayerHealthChanged?.Invoke(currentHealth);

        if (currentHealth == 0)
        {
            Die();
            return;
        }

        if (currentHealth == 1)
            BeforeDieMusic();
    }



    public void Die()
    {
        _animator.enabled = false;
       

        if (isDead)
        {
            return;
        }

        isDead = true;
        Debug.Log("isDead" + isDead);

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

        


        _animator.enabled = false;
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
    
       

        
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Taking damage");
            playerHealthController.TakeDamage(1);
        }
        if (other.gameObject.tag == "healthGem")
        {
            Debug.Log("player earned some health");
            playerHealthController.AddHealth(1);
        }
        else if(other.gameObject.tag == "shootingSpeedGem")
        {
            Debug.Log("Shooting speed gained");
            _detectTarget.AddShootingIntervalSpeed(-0.1f);
        }
        else if (other.gameObject.tag == "characterSpeedGem")
        {
            //Todo timer please
            _movementJoystick.AddMoveSpeed(4);
        }
        else
        {
            Debug.Log("player earned Some move speed");
            
        }
    }
}