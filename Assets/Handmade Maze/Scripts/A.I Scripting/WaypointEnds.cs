using System;
using Objectives;
using UnityEngine;

namespace AI
{
    public class WaypointEnds : MonoBehaviour
    {
        [SerializeField] Door.Door door;
        public Vector3 Position => transform.position; // LAMBDAS r cool
        
        //Simply a way to find any object marked as a waypoint

        // This just helps to see where the points are in the world.
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Hero")
            {
                GameVariables.keyItems--;
                door.open = true;
            }
        }
    }
}