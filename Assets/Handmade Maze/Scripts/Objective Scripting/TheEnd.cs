using UnityEngine;
public class TheEnd : MonoBehaviour
{
   [SerializeField] private GameObject end;

   private void Start()
   {
      end.SetActive(true);
   }
}
