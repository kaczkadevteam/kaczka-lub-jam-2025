using UnityEngine;

public class BlowerMovement : MonoBehaviour
{
    [SerializeField] private float blowerRotationSpeed;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, blowerRotationSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, -blowerRotationSpeed, 0);
        }
    }
}
