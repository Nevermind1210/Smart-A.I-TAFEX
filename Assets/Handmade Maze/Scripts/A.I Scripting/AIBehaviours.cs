using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIBehaviours : MonoBehaviour
    {
        public Transform hero;
        public Transform myTransform;
    
        public Wandering wandering;
        private Animator anim;
        private Rigidbody rb;
        private NavMeshAgent agent;

        private void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
        }

        public void Update()
        {
            ChasingHero();
            ReturnToWander();
        }

        public void ChasingHero() 
        {
            wandering.enabled = false;
            agent.speed = 5f;
            transform.LookAt(hero);
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }

        public void ReturnToWander()
        {
            wandering.enabled = true;
        }
    }
}