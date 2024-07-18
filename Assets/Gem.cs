using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
   public void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         Destroy(gameObject);
      }
      
   }
}
