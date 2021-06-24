using System;
using UnityEngine;
using Objectives;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        public bool endGame;
        private void Start()
        {
            if (endGame)
            {
                GameVariables.keyItems = 4;
                GetComponent<ObjectiveEnablerDisabler>().endingWaypointSet.SetActive(true);
            }
            else
            {
                GetComponent<ObjectiveEnablerDisabler>().keyWaypointSet.SetActive(true);
                GetComponent<ObjectiveEnablerDisabler>().endingWaypointSet.SetActive(false);
            }
        }
    }
}