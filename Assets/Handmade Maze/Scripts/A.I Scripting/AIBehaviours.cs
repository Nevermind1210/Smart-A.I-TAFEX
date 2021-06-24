using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace AI
{
   public enum States
    {
        Collecting,
        Finishing,
    }

    // The delegate that dictates what the functions for each state will look like.
    public delegate void StateDelegate();

    [System.Serializable]
    public class AIBehaviours
    {
        private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();

        [SerializeField] public States currentState = States.Collecting;

        public NavMeshAgent agent;

        [Header("Waypoints")] 
        public WaypointEnds[] endWaypoints;
        public WaypointKeys[] keyWaypoints;
        [Header("Waypoint Indexes")] 
        public int keyIndex = 1;
        public int endIndex = 0;

        // This is used to change the state from anywhere within the code that has reference to the state machine.
        public void ChangeState(States _newState)
        {
            if (_newState != currentState)
                currentState = _newState;
        }


        // Start is called before the first frame update
        public void Start(NavMeshAgent _agent)
        {
            agent = _agent;

            states.Add(States.Collecting, Collecting);
            states.Add(States.Finishing, Finishing);

            keyIndex = Mathf.Clamp(endIndex, 0, 4);
        }

        // Update is called once per frame
        public void Update()
        {
            // These 2 lines are what actually runs the state machine. It works by attempting to retrive the relevent function for the current state
            //then run the function if it successfully finds it.
            if (states.TryGetValue(currentState, out StateDelegate state))
                state.Invoke();
            else
                Debug.LogError($"No state function set for state {currentState}.");
        }

        // The function that will run when we are in the Collecting state.
        private void Collecting()
        {
            if (keyIndex <= 4)
            {
                WaypointKeys keyCurrentWaypoint = keyWaypoints[keyIndex - 1];
                
                agent.SetDestination(keyCurrentWaypoint.transform.position);
            }
            else if (keyIndex == 4)
            {
                SmartHeroAI.collectiablesFound = true;
                ChangeState(States.Finishing);
            }
        }

        // The function that will run when we are in the Finishing state.
        private void Finishing()
        {
            WaypointEnds endCurrentWaypoint = endWaypoints[endIndex];
            agent.SetDestination(endCurrentWaypoint.transform.position);

            if(!agent.pathPending && agent.remainingDistance < 0.01f)
            {
                endCurrentWaypoint.gameObject.SetActive(false);
            }
        }
    }
}