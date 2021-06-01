using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Door
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject leftDoorFrame;
        [SerializeField] private GameObject rightDoorFrame;
        [SerializeField] private GameObject doorPart;
        //localpos.y of the door of at its lowest
        private float closedDoorHeight;
        //localpos.y of the door at its highest.
        [SerializeField] private float openDoorHeight = 10;
        //the width of the door
        [SerializeField] private float doorWidth = 10;
        [SerializeField] private float doorMoveSpeed = 3;
        private float DoorYpos => doorPart.transform.localPosition.y;

        private MeshRenderer leftDoorFrameMeshRenderer;
        private MeshRenderer rightDoorFrameMeshRenderer;

        [System.NonSerialized] public bool open = false;

        private float TargetHeight
        {
            get
            {
                if (open)
                    return openDoorHeight;
                else
                    return closedDoorHeight;
            }
            set { }
        }

        private void OnValidate()
        {
            Vector3 leftDoorFramePosition = Vector3.left * doorWidth * 0.5f + Vector3.up * openDoorHeight * 0.5f;
            leftDoorFrame.transform.localPosition = new Vector3(-doorWidth * 0.5f, openDoorHeight * 0.5f, 0);
            rightDoorFrame.transform.localPosition = new Vector3(doorWidth * 0.5f, openDoorHeight * 0.5f, 0);
            
            //setting the scale of the doorframes to fit the height of the opened door;
            Vector3 doorFrameScale = new Vector3(1, openDoorHeight, 1);
            leftDoorFrame.transform.localScale = doorFrameScale;
            rightDoorFrame.transform.localScale = doorFrameScale;

            doorPart.transform.localPosition = new Vector3(0, openDoorHeight * 0.5f);
            doorPart.transform.localScale = new Vector3(1 * doorWidth - 1, openDoorHeight, 1);
        }
        
        private void SetDoorHeight(float _yPos) => doorPart.transform.localPosition =
            new Vector3(doorPart.transform.localPosition.x, _yPos, doorPart.transform.localPosition.z);
        public void SetColor(Color color) 
        { 
            leftDoorFrameMeshRenderer.material.color = color;
            rightDoorFrameMeshRenderer.material.color = color;
        }
        
        private void Start()
        {
            closedDoorHeight = DoorYpos;
            leftDoorFrameMeshRenderer = leftDoorFrame.GetComponent<MeshRenderer>();
            rightDoorFrameMeshRenderer = rightDoorFrame.GetComponent<MeshRenderer>();
            SetColor(Color.red);
        }

        void Update()
        {
            //if the door's height is below its target, move it up until it isn't; vice versa for if it is above it's target.
            if (DoorYpos < TargetHeight)
            {
                doorPart.transform.localPosition += Vector3.up * (Time.deltaTime * doorMoveSpeed);
                if (DoorYpos > TargetHeight)
                    SetDoorHeight(TargetHeight);
                
            }
            else if (DoorYpos > TargetHeight)
            {
                doorPart.transform.localPosition -= Vector3.up * (Time.deltaTime * doorMoveSpeed);
                if (DoorYpos < TargetHeight)
                    SetDoorHeight(TargetHeight);
            }
        }
    }
}