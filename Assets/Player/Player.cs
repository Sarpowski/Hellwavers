using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour , IKillable , IDamageble<int>
{
    [SerializeField]  private CharacterMovementJoystick _movementJoystick;
    [SerializeField] private PlayerHealthController   playerHealthController;
    [SerializeField]private Animator _animator;
    void Start()
    {    
        _movementJoystick = GetComponent<CharacterMovementJoystick>();
        playerHealthController = GetComponent<PlayerHealthController>();
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
        
    }

    private void PlayerHealthControllerOnTakeDamage(int currentHealth)
    {
        Debug.Log("in event send a damage info");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H tusuna basildi");
            playerHealthController.Damage(1);
            Debug.Log(playerHealthController.getHealth());
        }
        //damage burda handle edilcek fonksiyon ile
    }
    
    void FixedUpdate()
    {
        // Movement logic
        _movementJoystick.Move();
    }

    public void Damage(int damageAmount)
    {
        
            Debug.Log("dead");
        
    }

    public void Kill()
    {
        Debug.Log("kill");
    }
    
   
}
