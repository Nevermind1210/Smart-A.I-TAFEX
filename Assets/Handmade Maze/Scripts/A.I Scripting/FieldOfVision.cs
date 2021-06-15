using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class FieldOfVision : MonoBehaviour
{
    public AIBehaviours state;
    public float mRaycastRadius;
    public float mTargetDetectionDistance;

    private RaycastHit hitinfo;

    private bool hasDetectedHero = false;

    private void Update()
    {
        CheckForTargetLineOfSight(); 
    }

    public void CheckForTargetLineOfSight()
    {
        hasDetectedHero = Physics.SphereCast(transform.position, mRaycastRadius, transform.forward, out hitinfo, mTargetDetectionDistance);

        if(hasDetectedHero)
        {
            if(hitinfo.transform.CompareTag("Hero"))
            {
                Debug.Log("Detected Hero");
                // Logic that makes the threat start RUNNING and attacking when the Hero is in a certain range.
                state.ChasingHero();
            }
            else
            {
                Debug.Log("No Hero here moving on....");
               // add logic that stops chasing and resume back a state.
               state.ReturnToWander();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(hasDetectedHero)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawCube(new Vector3(0f, 0f, mTargetDetectionDistance / 2), new Vector3(mRaycastRadius, mRaycastRadius, mTargetDetectionDistance));
    }
}
