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
        private List<WaypointKeys> waypointsKey;
        private List<WaypointEnds> waypointEnds;
        private NavMeshLink jumpLink;
        private Animator _anim;

        //Will give us a random waypoint in the array as a variable
        private WaypointKeys RandomPoint => waypointsKey[Random.Range(0, waypointsKey.Count)];
        private WaypointEnds endPoints => waypointEnds[Random.Range(0, waypointEnds.Count)];

        public void RemoveWaypoint(WaypointKeys _waypointKeys) => waypointsKey.Remove(_waypointKeys);
        public void RemoveWaypointEnd(WaypointEnds _waypointEnds) => waypointEnds.Remove(_waypointEnds);
        
        // Start is called before the first frame update
        void Start()
        {
            _anim = gameObject.GetComponent<Animator>();
            jumpLink = gameObject.GetComponent<NavMeshLink>();
            agent = gameObject.GetComponent<NavMeshAgent>();
            // FindObjectsOfType gets every instance of this component in the scene
            waypointsKey = new List<WaypointKeys>(FindObjectsOfType<WaypointKeys>());
            waypointEnds = new List<WaypointEnds>(FindObjectsOfType<WaypointEnds>());
        }

        // Update is called once per frame
        void Update()
        {
            KeyWaypointsPath();
        }

        private void KeyWaypointsPath()
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
            else
            {
                EndingWaypointsPath();
            }
            if (agent.isPathStale)
            {
                agent.SetDestination(RandomPoint.Position);
            }
        }

        private void EndingWaypointsPath()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                if (!agent.hasPath) // Tell the agent if the path is not reachable then find another waypoint that HAS a path
                {
                    agent.SetDestination(endPoints.Position);
                }
                _anim.SetTrigger("Walk");
            }
            else if(endPoints == null)
            {
                _anim.SetTrigger("Idle");
            }
            if (agent.isPathStale)
            {
                agent.SetDestination(endPoints.Position);
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