using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace AI
{
   public enum States
    {
        MainPathing,
        FindingKeys,
        FindingPressurePlates,
    }

    // The delegate that dictates what the functions for each state will look like.
    public delegate void StateDelegate();

    [System.Serializable]
    public class AIBehaviours
    {
        private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();

        [SerializeField] public States currentState = States.MainPathing;

        public NavMeshAgent agent;

        [Header("Waypoints")] 
        public Waypoints[] waypoints;
        public List<WaypointEnds> endWaypoints;
        public WaypointKeys[] waypointKeys;
        [Header("Waypoint Indexes")] 
        public int keyIndex = 0;
        public int waypointIndex = 1;
        public int pressurePlateIndex = 0;

        //public void RemoveWaypoint(WaypointKeys _waypointKeys) => keyWaypoints.Remove(_waypointKeys);
        
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

            states.Add(States.MainPathing, MainPathing);
            states.Add(States.FindingKeys, FindingKeys);
            states.Add(States.FindingPressurePlates, FindingPressurePlate);

            keyIndex = Mathf.Clamp(keyIndex, 0, 5);
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

        // The function that will run when we are in the MainPathing state.
        private void MainPathing() 
        {
            if (keyIndex <= 3) // this will always run 
            {
                ChangeState(States.FindingKeys);
            }
            else if (pressurePlateIndex <= 2) // then drop down and run until thats max value
            {
               ChangeState(States.FindingPressurePlates);
            }
            else if(waypointIndex <= 1) // and then we finish this!
            {
                Waypoints _waypoints = waypoints[waypointIndex - 1];
                agent.SetDestination(_waypoints.transform.position);
            }
        }
        
        // Function runs when state is Switched to FindingKeys
        private void FindingKeys()
        {
            if (keyIndex <= 3)
            {
                WaypointKeys _waypointKeys = waypointKeys[keyIndex]; // getting the index of that and then 
                agent.SetDestination(_waypointKeys.transform.position);

                if (!agent.pathPending && agent.remainingDistance < 0.01f)
                {
                    keyIndex += 1;
                    ChangeState(States.FindingKeys);

                    _waypointKeys.gameObject.SetActive(false);
                }
            }
            // Once all keys are found
            else
            {
                SmartHeroAI.collectiblesFound = true;
                ChangeState(States.FindingPressurePlates);
            }
        }
        
        private void FindingPressurePlate()
        {
            Debug.Log("Finding Next Pressure Plate");
            if (pressurePlateIndex <= 2)
            {
                WaypointEnds _waypointEnds = endWaypoints[pressurePlateIndex];
                agent.SetDestination(_waypointEnds.transform.position);

                if (!agent.pathPending && agent.remainingDistance < 0.01f)
                {
                    pressurePlateIndex += 1;
                    ChangeState(States.FindingKeys);

                    _waypointEnds.gameObject.SetActive(false);
                }
            }
            // Once all Plates are found
            else
            {
                ChangeState(States.MainPathing);
            }
        }
    }
}