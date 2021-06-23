using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace AI
{
   public class FieldOfVision : MonoBehaviour
   {
   
      public float radius;
      [Range(0,360)]   
      public float angle;

      public GameObject playerRef;  

      public LayerMask targetMask; // this makes things easier to sort and helps the enemy know thats the only mask to check for....
      public LayerMask obstructionMask; // additionally adding this allows the enemy recognise obstructions and therefore prevents wallhaxs...

      public bool canSeeHero;

      private void Start()
      {
         StartCoroutine(FOVRoutine()); 
         playerRef = GameObject.FindGameObjectWithTag("Hero"); // caching in the AI Hero....
      }

      private IEnumerator FOVRoutine() // This Routine prevents every frame to be checked and only timed to be quarter of a second 
      {
         WaitForSeconds wait = new WaitForSeconds(0.2f);
      
         while (true)
         {
            yield return wait;
            FieldOfViewCheck();
         }
         // ReSharper disable once IteratorNeverReturns
      }

      private void FieldOfViewCheck()
      {
         Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

         if (rangeChecks.Length != 0)
         {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
               float distanceToTarget = Vector3.Distance(transform.position, target.position);

               if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                  canSeeHero = true;
               else
                  canSeeHero = false;
            }
            else
               canSeeHero = false;
         }
         else if(canSeeHero)
            canSeeHero = false;
      }
   }
}