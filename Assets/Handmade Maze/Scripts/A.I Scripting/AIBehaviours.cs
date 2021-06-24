using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;

namespace AI
{
   public enum States
    {
        Chase,
        Wander,
        Collecting,
        Finish
    }

    // The delegate that dictates what the functions for each state will look like.
    public delegate void StateDelegate();

    public class AIBehaviours : MonoBehaviour
    {
        [SerializeField] private Wandering wanderer;
        [SerializeField] private ChaseBehaviour chaser;
        
        private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();

        [SerializeField] private States currentState = States.Wander;
        //[SerializeField] private Transform controlled; //The thing that will be affected by our state machine

        // This is used to chnage the state from anywhere within the code that has reference to the state machine.
        public void ChangeState(States _newState)
        {
            if (_newState != currentState)
                currentState = _newState;
        }


        // Start is called before the first frame update
        void Start()
        {
            // This is the same as checking if the variable is null, then setting it, otherwise retain the value.
            states.Add(States.Chase, Chase);
            states.Add(States.Wander, Wander);
            /*states.Add(States.Collecting, Collecting);
            states.Add(States.Finish, Finshing);*/
            
            chaser.Start(gameObject);
            wanderer.Start(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            // These 2 lines are what actually runs the state machine. It works by attempting to retrive the relevent function for the current state
            //then run the function if it successfully finds it.
            if (states.TryGetValue(currentState, out StateDelegate state))
                state.Invoke();
            else
                Debug.LogError($"No state function set for state {currentState}.");
        }
        // The function that will run when we are in the Rotate state.
        private void Chase() => chaser.Update();
        // The function that will run when we are in the scale state.
        private void Wander() => wanderer.Update();
    }
}