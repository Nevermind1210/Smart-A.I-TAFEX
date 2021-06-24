using System;
using Objectives;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SmartHeroAI : MonoBehaviour
    {
        public GameObject collectibleCount;
        
        public AIBehaviours stateBehaviours;

        public NavMeshAgent agent;
        public static bool collectiablesFound;
        [SerializeField] private GameObject gameOver;


        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            
            stateBehaviours.Start(agent);

            collectiablesFound = false;
        }

        void Update()
        {
            stateBehaviours.Update();
            
            CollectingPath();
            SwitchStates();
        }
        
        public void SwitchStates()
        {
            if (stateBehaviours.currentState == States.Collecting)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    stateBehaviours.ChangeState(States.Collecting);
                }
                if (stateBehaviours.keyIndex == 4 && agent.remainingDistance <= 5 && !collectiablesFound)
                {
                    stateBehaviours.ChangeState(States.Finishing);
                }
            }
        }

        public void CollectingPath()
        {
            if (stateBehaviours.currentState == States.Collecting)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    stateBehaviours.keyIndex += 1;
                    GameVariables.keyItems += 1;
                    collectibleCount.GetComponent<TextMeshProUGUI>().text = GameVariables.keyItems + "/4 keys collected";
                    stateBehaviours.ChangeState(States.Collecting);
                }
                if (!agent.pathPending && agent.remainingDistance < 0.01f && stateBehaviours.keyIndex == 4)
                {
                    stateBehaviours.ChangeState(States.Finishing);
                }
            }
        }
        
        private void GameOver()
        {
            gameOver.SetActive(true);
            Time.timeScale = 0.0f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Hero")
            {
                GameOver();
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