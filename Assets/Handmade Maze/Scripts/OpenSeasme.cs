using System;
using System.Collections;
using System.Collections.Generic;
using Objectives;
using UnityEngine;

namespace Door
{
    public class OpenSeasme : MonoBehaviour
    {
        private Door door;
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Hero" && GameVariables.keyItems > 0)
            {
                GameVariables.keyItems--;
                door.open = true;
            }
        }
    }
}