using System;
using System.Collections;
using System.Collections.Generic;
using Objectives;
using UnityEngine;

namespace Door
{
    public class OpenSeasme : MonoBehaviour
    {
        [Header("Door to open!")]
        [SerializeField] Door door;
        private void OnTriggerEnter(Collider collider)
        {
            StartCoroutine(OpenDoor());
        }

        private IEnumerator OpenDoor()
        {
            door.open = true;
            yield return new WaitForFixedUpdate();
        }
    }
}