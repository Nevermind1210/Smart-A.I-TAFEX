using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentSmith : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Waypoint[] waypoints;
        private NavMeshLink jumpLink;
        private Animator _anim;

        //Will give us a random waypoint in the array as a variable
        private Waypoint RandomPoint => waypoints[Random.Range(0, waypoints.Length)];

        // Start is called before the first frame update
        void Start()
        {
            _anim = gameObject.GetComponent<Animator>();
            jumpLink = gameObject.GetComponent<NavMeshLink>();
            agent = gameObject.GetComponent<NavMeshAgent>();
            // FindObjectsOfType gets every instance of this component in the scene
            waypoints = FindObjectsOfType<Waypoint>();
        }

        // Update is called once per frame
        void Update()
        {
            // Has the agent reached it's position?
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
               
                if (!agent.hasPath) // Tell the agent if the path is not reachable then find another waypoint that HAS a path
                {
                    agent.SetDestination(RandomPoint.Position);
                }
                _anim.SetTrigger("Walk");
            }
            else if(waypoints == null)
            {
                _anim.SetTrigger("Idle");
            }
            
            if (agent.isPathStale)
            {
                agent.SetDestination(RandomPoint.Position);
            }
                
            if (agent.isOnOffMeshLink)
            {
                if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Jumpin"))
                {
                    _anim.SetTrigger("Jump");
                }
                
            }
        }
        
        void OnDrawGizmosSelected()
        {
            if (agent != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(agent.destination, 1f);
                
                Gizmos.DrawLine(transform.position, agent.destination);
                
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(agent.steeringTarget, 1f);
            }
        }
    }
}