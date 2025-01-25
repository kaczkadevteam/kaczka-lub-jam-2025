using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    private static GlobalConfig _instance;

    public static GlobalConfig Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public float blowerOrbitingRadius = 10f;

    [Header("Stuff relative to blower orbiting radius")]
    public float bubbleSpawnOffsetFraction = 0.4f;
    public float volcanoSizeFraction = 1f;

    [Header("Enemy spawning")]
    public float enemySpawnRateGrowth = 0.05f;

    [Header("Lose time slow")]
    public float timeScaleValueToStopCoroutine = 0.001f;
    public float dividerOfTimeScaleOnLose = 2f;
    public float secondsBetweenTimeScaleDivision = 0.25f;

    [Header("BubbleSpawner settings")]
    public float minTimeSpawnInterval = 0.005f;
    public float baseTimeSpawnInterval = 0.5f;

    [Header("Upgrade settings")]
    public int upgradeDropChance = 10;
}
