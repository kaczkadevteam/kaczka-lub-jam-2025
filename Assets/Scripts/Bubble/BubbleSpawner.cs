using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField]
    public Transform blower;

    [SerializeField]
    public Transform bubbleParent;

    [SerializeField]
    private GameObject bubblePrefab;

    private float timeSinceLastSpawn = 0.0f;

    private float GetTimeToSpawn()
    {
        float baseInterval = GlobalConfig.Instance.baseTimeSpawnInterval;
        float upgradeValue =
            BubbleSpawnerUpgradeManager.Instance.bubbleSpawnerSpawnSpeedUpgrade.GetUpgradeValue();

        // Apply diminishing returns using a logarithmic function
        float diminishingReturnFactor = Mathf.Log(1 + upgradeValue, 2); // Log base 2 for diminishing returns
        float calculatedTime = baseInterval - diminishingReturnFactor;

        return Mathf.Max(calculatedTime, GlobalConfig.Instance.minTimeSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it is time to spawn a bubble
        timeSinceLastSpawn = Time.deltaTime + timeSinceLastSpawn;

        if (timeSinceLastSpawn > GetTimeToSpawn())
        {
            // Spawn a bubble and reset the timer
            SpawnBubble();
            timeSinceLastSpawn = 0;
        }
    }

    void SpawnBubble()
    {
        if (GameManager.Instance.BabyHealth <= 0) return;

        var bubbleSpawnOffsetX = Random.Range(
            -GlobalConfig.Instance.blowerOrbitingRadius
                * GlobalConfig.Instance.bubbleSpawnOffsetFraction,
            GlobalConfig.Instance.blowerOrbitingRadius
                * GlobalConfig.Instance.bubbleSpawnOffsetFraction
        );
        var bubbleSpawnOffsetZ = Random.Range(
            -GlobalConfig.Instance.blowerOrbitingRadius
                * GlobalConfig.Instance.bubbleSpawnOffsetFraction,
            GlobalConfig.Instance.blowerOrbitingRadius
                * GlobalConfig.Instance.bubbleSpawnOffsetFraction
        );

        var spawnPoint = new Vector3(
            transform.position.x + bubbleSpawnOffsetX,
            transform.position.y,
            transform.position.z + bubbleSpawnOffsetZ
        );

        bubblePrefab.GetComponent<Bubble>().blower = blower;
        Instantiate(bubblePrefab, spawnPoint, Quaternion.identity, bubbleParent);
    }
}
