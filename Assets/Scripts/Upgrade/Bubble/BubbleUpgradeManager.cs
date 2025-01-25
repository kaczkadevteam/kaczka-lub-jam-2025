using UnityEngine;

public class BubbleUpgradeManager : MonoBehaviour
{
    private static BubbleUpgradeManager _instance;

    public static BubbleUpgradeManager Instance => _instance;

    public BubbleSpeedUpgrade bubbleSpeedUpgrade;

    public BubbleSizeUpgrade bubbleSizeUpgrade;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
