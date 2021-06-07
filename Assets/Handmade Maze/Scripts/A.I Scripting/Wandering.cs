using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

namespace AI
{
   public class Wandering : MonoBehaviour
   {
      public float wanderRadius;
      public float wanderTimer;

      private Transform target;
      private NavMeshAgent agent;
      private float timer;
      private Animator _anim;
   
      // Initialization of the agent
      private void OnEnable()
      {
         agent = GetComponent<NavMeshAgent>();
         _anim = GetComponent<Animator>();
         timer = wanderTimer;
      }

      private void Update()
      {
         timer += Time.deltaTime;

         if (timer >= wanderTimer)
         {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
               Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
               agent.SetDestination(newPos);
               timer = 0;
            }
            _anim.SetTrigger("Z_Walking");
         }
         else if (target == null)
         {
            _anim.SetTrigger("Z_Idle");
         }
      }

      public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
      {
         
         Vector3 randDirection = Random.insideUnitSphere * dist;

         randDirection += origin;

         NavMeshHit navHit;

         NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

         return navHit.position;
      }
   }
}