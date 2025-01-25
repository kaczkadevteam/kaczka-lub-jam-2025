using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlowerMovement : MonoBehaviour
{
    [SerializeField] private float blowerRotationSpeed;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetMouseButton(0) || Gamepad.current?.leftStick.ReadValue()[0] < 0)
        {
            transform.Rotate(0, blowerRotationSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetMouseButton(1) || Gamepad.current?.leftStick.ReadValue()[0] > 0)
        {
            transform.Rotate(0, -blowerRotationSpeed, 0);
        }
    }
}
