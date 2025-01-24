using UnityEngine;

public class BlowerMovement : MonoBehaviour
{
    [SerializeField] private float blowerRotationSpeed;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, blowerRotationSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, -blowerRotationSpeed, 0);
        }
    }
}
