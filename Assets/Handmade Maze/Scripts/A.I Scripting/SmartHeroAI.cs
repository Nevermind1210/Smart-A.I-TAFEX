using System;
using Objectives;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SmartHeroAI : MonoBehaviour
    {
        public AudioMixer masterMixer;
        public AudioSource winMusic;
        public GameObject collectibleCount;
        
        public AIBehaviours stateBehaviours;

        public NavMeshAgent agent;
        public static bool collectiblesFound;
        [SerializeField] private GameObject gameOver;

        //public void RemoveWaypoint(WaypointKeys _waypointKeys) => stateBehaviours.RemoveWaypoint(_waypointKeys);

        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            
            stateBehaviours.Start(agent);

            collectiblesFound = false;
        }

        void Update()
        {
            stateBehaviours.Update();
            
            MainPath();
            SwitchStates();
        }
        
        public void SwitchStates()
        {
            if (stateBehaviours.currentState == States.MainPathing)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    stateBehaviours.ChangeState(States.MainPathing);
                }
                if (stateBehaviours.keyIndex == 4 && agent.remainingDistance <= 5 && !collectiblesFound)
                {
                    stateBehaviours.ChangeState(States.MainPathing);
                }
            }
        }

        public void MainPath()
        {
            if (stateBehaviours.currentState == States.MainPathing)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    stateBehaviours.waypointIndex += 1;
                    GameVariables.keyItems += 1;
                    collectibleCount.GetComponent<TextMeshProUGUI>().text = GameVariables.keyItems + "/4 keys collected";
                    stateBehaviours.ChangeState(States.MainPathing);
                }
                // If path is blocked
                if (agent.hasPath && agent.path.status == NavMeshPathStatus.PathPartial &&
                    agent.remainingDistance <= 10)
                {
                    stateBehaviours.ChangeState(States.FindingPressurePlates);
                }
                // if path is unreachable....
                if (agent.hasPath && agent.path.status == NavMeshPathStatus.PathInvalid &&
                    agent.remainingDistance <= 10)
                {
                    stateBehaviours.ChangeState(States.FindingPressurePlates);
                }
                //finish the game once we reached every waypoint
                if (agent.pathPending && agent.remainingDistance < 0.01f && stateBehaviours.waypointIndex == 1)
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            gameOver.SetActive(true);
            //Time.timeScale = 0.0f; // this bloody line stops sounds too! PESKY LITTLE THING
            Cursor.lockState = CursorLockMode.None;
            masterMixer.SetFloat("BGM", -60f);
            masterMixer.SetFloat("winMusic", -18f);
            winMusic.Play();
            Cursor.visible = true;
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