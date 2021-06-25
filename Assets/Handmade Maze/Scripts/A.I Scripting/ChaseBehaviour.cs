using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [System.Serializable]
    public class ChaseBehaviour : MonoBehaviour
    {
        private AIBehaviours state;
        private FieldOfVision fieldOfVision;
        [SerializeField] private Transform Hero;
        private Transform itself;
        private float moveSpeed = 3f; 
        private NavMeshAgent agent;
        
        public void Start()
        {
            itself = transform;
            agent = GetComponent<NavMeshAgent>();
            fieldOfVision = GetComponent<FieldOfVision>();
            state = GetComponent<AIBehaviours>();
        }

        /*public void Update()
        {
            agent.speed = moveSpeed;
            itself.LookAt(Hero);

            if (!agent.pathPending && agent.remainingDistance <= fieldOfVision.radius && fieldOfVision.canSeeHero)
            {
                if (!agent.hasPath && agent.remainingDistance >= fieldOfVision.radius)
                {
                    state.ChangeState(States.Chase);
                }
                else
                {
                    state.ChangeState(States.Wander);
                }
            }
        }*/
    }
}