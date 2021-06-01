using System.Collections;
using UnityEngine;

namespace Controller
{
    public class FlyCamera : MonoBehaviour
    {
        public float cameraSensitivity = 90;
        public float climbSpeed = 4;
        public float normalMovespeed = 10;
        public float slowMoveFactor = 0.25f;
        public float fastMoveFactor = 3;

        private float rotationX = 0.0f;
        private float rotationY = 0.0f;

        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position += transform.forward * (normalMovespeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * (normalMovespeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
            {
                transform.position += transform.forward * (normalMovespeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * (normalMovespeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            }
            else
            {
                transform.position += transform.forward * (normalMovespeed * normalMovespeed) * Input.GetAxis("Vertical") * Time.deltaTime;
                transform.position += transform.right * (normalMovespeed * normalMovespeed) * Input.GetAxis("Horizontal") * Time.deltaTime;
            }

            if(Input.GetKey (KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime;}
            if(Input.GetKey (KeyCode.E)) { transform.position += transform.up * climbSpeed * Time.deltaTime;}

            if(Input.GetKeyDown (KeyCode.End))
            {
                if (Cursor.visible == true)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if(Cursor.visible == false)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}