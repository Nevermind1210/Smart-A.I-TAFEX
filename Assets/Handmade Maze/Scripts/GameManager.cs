using System;
using AI;
using UnityEngine;
using Objectives;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        public bool endGame; 
        private AgentSmith smitty;
        private ObjectiveEnablerDisabler ObjectiveObject;
        private void Start()
        {
            smitty = GetComponent<AgentSmith>();
            ObjectiveObject = GetComponent<ObjectiveEnablerDisabler>();
            if (endGame)
            {
                GameVariables.keyItems = 4;
                
                for (int index = ObjectiveObject.keyWaypointSet.transform.childCount
                    ;index >=0
                    ;index--)
                {
                    Transform child = ObjectiveObject.keyWaypointSet.transform.GetChild(index);
                    
                    if(child == null) continue;

                    WaypointKeys key = child.GetComponent<WaypointKeys>();

                    if(key == null) continue;
                    
                    smitty.RemoveWaypoint(key);
                }

                // foreach (Transform child in ObjectiveObject.endingWaypointSet.transform)
                // {
                //     smitty.RemoveWaypoint(child);
                //     ObjectiveObject.enabled = false;
                // }
                ObjectiveObject.endingWaypointSet.SetActive(true);
                ObjectiveObject.keyWaypointSet.SetActive(false);
            }
            else
            {
                ObjectiveObject.keyWaypointSet.SetActive(true);
                ObjectiveObject.endingWaypointSet.SetActive(false);
            }
        }
    }
}