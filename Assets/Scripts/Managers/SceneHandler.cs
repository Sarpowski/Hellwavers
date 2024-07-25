using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    
    public void RestartGame()
    {
        //TODO load level 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Future Implementation for the Start Game logic 
    public void MenuGame()
    {
        //could be a menu scene
        // SceneManager.LoadScene("Main Menu");
    }
}
