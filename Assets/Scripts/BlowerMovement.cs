using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlowerMovement : MonoBehaviour
{
    [SerializeField]
    private float baseBlowerRotationSpeed;

    float GetBlowerRotationSpeedUpgrade()
    {
        var blowerRotationSpeedUpgrade = BlowerUpgradeManager.Instance.blowerRotationSpeedUpgrade;
        var blowerRotationSpeedUpgradeModifierValue = blowerRotationSpeedUpgrade.GetUpgradeValue();

        return baseBlowerRotationSpeed + blowerRotationSpeedUpgradeModifierValue;
    }

    void FixedUpdate()
    {
        var blowerRotationSpeed = GetBlowerRotationSpeedUpgrade();

        if (
            Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetMouseButton(0)
            || Gamepad.current?.leftStick.ReadValue()[0] < -0.1
        )
        {
            transform.Rotate(0, blowerRotationSpeed, 0);
        }
        if (
            Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.RightArrow)
            || Input.GetMouseButton(1)
            || Gamepad.current?.leftStick.ReadValue()[0] > 0.1
        )
        {
            transform.Rotate(0, -blowerRotationSpeed, 0);
        }
    }
}
