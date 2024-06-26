using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour , IKillable , IDamageble<int>
{
    [SerializeField] private CharacterMovementJoystick _movementJoystick;
    [SerializeField] private PlayerHealthController   playerHealthController;
    [SerializeField] private Animator _animator;
    void Start()
    {    
        _movementJoystick = GetComponent<CharacterMovementJoystick>();
        playerHealthController = GetComponent<PlayerHealthController>();
        playerHealthController.initHealth();

        playerHealthController.HealthZero += PlayerHealthControllerOnHealthZero;
    }

    private void PlayerHealthControllerOnHealthZero()
    {
        // TODO
        // durumlari ekle
        //  
        
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
