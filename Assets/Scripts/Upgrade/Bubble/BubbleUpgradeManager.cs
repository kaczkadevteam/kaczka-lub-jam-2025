using UnityEngine;

public class BubbleUpgradeManager : MonoBehaviour
{
    private static BubbleUpgradeManager _instance;

    public static BubbleUpgradeManager Instance => _instance;

    public BubbleSpeedUpgrade bubbleSpeedUpgrade;

    public BubbleSizeUpgrade bubbleSizeUpgrade;

    public BubbleLifetimeUpgrade bubbleLifetimeUpgrade;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
