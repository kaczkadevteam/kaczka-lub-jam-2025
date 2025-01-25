using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private Transform blower;
    [SerializeField] private Transform bubbleParent;
    [SerializeField] private GameObject bubblePrefab;

    private float timeSinceLastSpawn = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // Check if it is time to spawn a bubble
        timeSinceLastSpawn = Time.deltaTime + timeSinceLastSpawn;
        
        if (timeSinceLastSpawn > BlowerUpgradeManager.Instance.blowerUpgrades.BubbleSpawnRateInterval)
        {
            // Spawn a bubble and reset the timer
            SpawnBubble();
            timeSinceLastSpawn = 0;
            
        }
    }

    void SpawnBubble()
    {
        var bubbleSpawnOffsetX = Random.Range(-GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction, GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction);
        var bubbleSpawnOffsetZ = Random.Range(-GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction, GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction);

        var spawnPoint = new Vector3(transform.position.x  + bubbleSpawnOffsetX, transform.position.y, transform.position.z  + bubbleSpawnOffsetZ);

        Instantiate(bubblePrefab, spawnPoint, Quaternion.identity, bubbleParent);
        bubblePrefab.GetComponent<Bubble>().blower = blower;
    }
}
