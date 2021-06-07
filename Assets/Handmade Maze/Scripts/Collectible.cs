using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.name == "Hero")
      {
         GameVariables.items += 1;
         Destroy(gameObject);
      }
   }
}
