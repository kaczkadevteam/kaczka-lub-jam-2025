using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    public Transform blower;

    [SerializeField]
    private Vector3 movementDirection;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float currentSpeed;

    [SerializeField]
    private float speedFractionBelowWhichToDisappear;

    [SerializeField]
    private float speedLossMultiplier;

    void InitBubbleUpgrades()
    {
        var bubbleUpgradeManager = BubbleUpgradeManager.Instance;

        InitBubbleSpeedUpgrade(bubbleUpgradeManager);
        InitBubbleSizeUpgrade(bubbleUpgradeManager);
    }

    void InitBubbleSpeedUpgrade(BubbleUpgradeManager bubbleUpgradeManager)
    {
        var bubbleSpeedUpgrade = bubbleUpgradeManager.bubbleSpeedUpgrade;
        var bubbleSpeedUpgradeModifierValue = bubbleSpeedUpgrade.GetUpgradeValue();
        Debug.Log(bubbleSpeedUpgrade.GetUpgradeValue());

        maxSpeed += bubbleSpeedUpgradeModifierValue;
    }

    void InitBubbleSizeUpgrade(BubbleUpgradeManager bubbleUpgradeManager)
    {
        var bubbleSizeUpgrade = bubbleUpgradeManager.bubbleSizeUpgrade;
        var bubbleSizeUpgradeModifierValue = bubbleSizeUpgrade.GetUpgradeValue();
        Debug.Log(bubbleSizeUpgrade.GetUpgradeValue());

        // Increase the size of the bubble by increasing the scale of the bubble
        transform.localScale += new Vector3(
            bubbleSizeUpgradeModifierValue,
            bubbleSizeUpgradeModifierValue,
            bubbleSizeUpgradeModifierValue
        );
    }

    void Start()
    {
        InitBubbleUpgrades();

        movementDirection = Vector3.Normalize(transform.position - blower.position);
        currentSpeed = maxSpeed;
    }

    void FixedUpdate()
    {
        transform.position += movementDirection * currentSpeed * Time.deltaTime;
        currentSpeed -= speedLossMultiplier * maxSpeed * Time.deltaTime;
        if (currentSpeed <= speedFractionBelowWhichToDisappear * maxSpeed)
        {
            Destroy(gameObject);
        }
    }
}
