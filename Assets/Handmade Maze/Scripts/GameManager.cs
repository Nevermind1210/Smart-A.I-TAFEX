﻿using System;
using AI;
using UnityEngine;
using Objectives;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        public GameObject collectibleCount;
        public bool endGame;
        public bool speedGame;
        [SerializeField] public float speedyBoi;
        private ObjectiveEnablerDisabler ObjectiveObject;
        [SerializeField] private AIBehaviours stateBehaviours;
        private void Start()
        {
            ObjectiveObject = GetComponent<ObjectiveEnablerDisabler>();
            if (endGame)
            {
                stateBehaviours.keyIndex = 4;
                
                ObjectiveObject.endingWaypointSet.SetActive(true);
                ObjectiveObject.keyWaypointSet.SetActive(false);
            }
        }

        private void Update()
        {
            if (speedGame)
            {
                Time.timeScale = speedyBoi;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}