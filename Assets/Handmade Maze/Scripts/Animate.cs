using UnityEngine;

namespace Controller
{
    public class Animate : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("xFloat", transform.position.x);
        }
    }
}