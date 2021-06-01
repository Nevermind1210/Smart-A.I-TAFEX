using UnityEngine;

public class SpinnyThingy : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed);
    }
}
