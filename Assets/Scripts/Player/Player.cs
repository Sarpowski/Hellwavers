using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerHealthController))]
[RequireComponent(typeof(CharacterMovementJoystick))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DetectTarget))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IKillable, IDamageble<int>
{
    private CharacterMovementJoystick _movementJoystick;
    private PlayerHealthController _playerHealthController;
    private Animator _animator;
    private DetectTarget _detectTarget;
    private Rigidbody _body;

    private bool _isActive;
    private bool _isInputActive;

    public bool IsDead { get; private set; }
    public event Action<int> PlayerHealthChanged;
    public event Action PlayerDied;

    private void Init()
    {
        _movementJoystick = GetComponent<CharacterMovementJoystick>();
        _playerHealthController = GetComponent<PlayerHealthController>();
        _animator = GetComponent<Animator>();
        _detectTarget = GetComponent<DetectTarget>();
        _body = GetComponent<Rigidbody>();
    }

    public void Initialize()
    {
        Init();
        SubscribeEvents();

        InitializeSubControllers();
    }

    private void InitializeSubControllers()
    {
        _playerHealthController.Initialize();
        _detectTarget.Initialize();
    }

    public void OnStartGameplay()
    {
        _playerHealthController.OnStartGameplay();
        _detectTarget.OnStartGameplay();
        
        _isActive = true;
        _isInputActive = true;

        SetJoystickState(true);
    }

    public void OnFinishGameplay(bool isSuccess)
    {
        _playerHealthController.OnFinishGameplay();
        _detectTarget.OnFinishGameplay();

        SetJoystickState(false);
    }
    private void SubscribeEvents()
    {
        UnsubscribeEvents();

        _playerHealthController.HealthChanged += OnPlayerHealthChanged;
    }

    private void UnsubscribeEvents()
    {
        _playerHealthController.HealthChanged -= OnPlayerHealthChanged;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H button is pressed");
            _playerHealthController.TakeDamage(1);

            Debug.Log(_playerHealthController.getHealth());
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
        if (!_isActive)
            return;

        if (_isInputActive)
        {
            // Movement logic
            _movementJoystick.Move();
        }
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
        if (IsDead)
        {
            Debug.LogError("Player is already dead!!!!");
            return;
        }

        IsDead = true;

        ResetVelocity();

        _animator.enabled = false;
        
        _isActive = false;
        PlayerDied?.Invoke();
    }

    private void SetJoystickState(bool isInputActive)
    {
        _movementJoystick.SetJoystickVisibility(isInputActive);
        _isInputActive = isInputActive;
    }

    private void ResetVelocity()
    {
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
    }

    public void TakeDamage(int damageAmount)
    {
        _playerHealthController.TakeDamage(damageAmount);
    }

    public void Kill()
    {
        _playerHealthController.TakeDamage(_playerHealthController.getHealth());
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Debug.Log("Taking damage");
            _playerHealthController.TakeDamage(1);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Debug.Log("Taking damage");
            _playerHealthController.TakeDamage(1);
        }

        if (other.gameObject.tag == "healthGem")
        {
            // Debug.Log("player earned some health");
            _playerHealthController.AddHealth(1);
        }
        else if (other.gameObject.tag == "shootingSpeedGem")
        {
            // Debug.Log("Shooting speed gained");
            _detectTarget.AddShootingIntervalSpeed(-0.1f);
        }
        else if (other.gameObject.tag == "characterSpeedGem")
        {
            //Todo timer please
            _movementJoystick.AddMoveSpeed(4);
        }
        else
        {
            // Debug.Log("player earned Some move speed");
        }
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}

public enum PlayerJoystickState
{
    Enabled,
    Disabled
}