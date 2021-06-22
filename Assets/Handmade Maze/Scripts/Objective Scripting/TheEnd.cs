using UnityEngine;
public class TheEnd : MonoBehaviour
{
   [SerializeField] private GameObject end;
   
   private void OnTriggerEnter(Collider collided)
     {
        if (collided.gameObject.tag == "Hero")
        {
           end.SetActive(true);
           Time.timeScale = 0f;
        }
     }
}
