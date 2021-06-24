using System;
using Objectives;
using UnityEngine;

namespace AI
{
    public class Waypoint : MonoBehaviour
    {
        
        public Vector3 Position => transform.position; // LAMBDAS r cool
        
        //Simply a way to find any object marked as a waypoint

        // This just helps to see where the points are in the world.
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Hero")
            { 
                other.GetComponent<AgentSmith>().RemoveWaypoint(this);
              Destroy(gameObject);
            }
        }
    }
}