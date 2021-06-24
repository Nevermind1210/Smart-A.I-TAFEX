using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Door
{
    [System.Serializable]
    public class DoorGroupHolder
    {
        public List<Door> Doors = new List<Door>(); 
    }
    public class DoorSystem : MonoBehaviour
    {
        [SerializeField] public List<DoorGroupHolder> DoorGroupHolders = new List<DoorGroupHolder>();

        [SerializeField, Tooltip("Time for the open doors to switch.")]
        private float doorSwitchTime;

        private bool switchDoorsComplete = true;

        private void Update()
        {
            if (switchDoorsComplete)
            {
                StartCoroutine(SwitchDoors());
                switchDoorsComplete = false;
            }
        }

        private IEnumerator SwitchDoors()
        {
            switchDoorsComplete = false;
            foreach (DoorGroupHolder doorGroupHolder in DoorGroupHolders)
            {
                List<Door> doors = doorGroupHolder.Doors;
                int doorIndexToOpen = Random.Range(0, doors.Count);
                int currentDoorIndex = 0;
                foreach (Door door in doors)
                {
                    door.open = false;
                    door.SetColor(Color.red);
                    if (currentDoorIndex == doorIndexToOpen)
                    {
                        door.open = true;
                        door.SetColor(Color.green);
                    }
                    currentDoorIndex += 1;
                }
            }

            yield return new WaitForSeconds(doorSwitchTime);
            switchDoorsComplete = true;
        }
    }
}