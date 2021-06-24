using AI;
using UnityEngine;

namespace Objectives
{
    public class ObjectiveEnablerDisabler : MonoBehaviour
    {
        [SerializeField] public GameObject keyWaypointSet;
        [SerializeField] public GameObject endingWaypointSet;
        private void Update()
        {
            if (GameVariables.keyItems == 4)
            {
                foreach (var child in keyWaypointSet.transform)
                { 
                   // GetComponent<AgentSmith>().RemoveWaypoint(child);
                }
                keyWaypointSet.SetActive(false);
                endingWaypointSet.SetActive(true);
            }
        }
    }
}