using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

namespace Objectives
{
   public class Collectible : MonoBehaviour
   {
      private void OnTriggerEnter(Collider collider)
      {
         if (collider.gameObject.tag == "Hero")
         {
            GameVariables.keyItems += 1;
            //collider.GetComponent<SmartHeroAI>().RemoveWaypoint(gameObject.GetComponentInParent<WaypointKeys>());
            //Destroy(gameObject);
         }
      }
   }
}