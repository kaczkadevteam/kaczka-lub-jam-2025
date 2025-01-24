using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public float spawnRate = .1f;
    private float nextSpawn = 0.0f;
    [SerializeField] private Transform baseSpawnPoint;
    [SerializeField] private Transform blower;
    [SerializeField] private Transform bubbleParent;
    public GameObject bubble;

    public int enemySpawnedCount = 0;

    // Update is called once per frame
    void Update()
    {
        // Check if it is time to spawn a bubble
        nextSpawn = Time.deltaTime + nextSpawn;
        
        if (nextSpawn > spawnRate)
        {
            // Spawn a bubble and reset the timer
            SpawnBubble();
            nextSpawn = 0;
            
        }
    }

    void SpawnBubble()
    {
        var bubbleSpawnOffsetX = Random.Range(-GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction, GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction);
        var bubbleSpawnOffsetZ = Random.Range(-GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction, GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction);

        var spawnPoint = new Vector3(baseSpawnPoint.position.x  + bubbleSpawnOffsetX, baseSpawnPoint.position.y, baseSpawnPoint.position.z  + bubbleSpawnOffsetZ);

        Instantiate(bubble, spawnPoint, Quaternion.identity, bubbleParent);
        bubble.GetComponent<Bubble>().blower = blower;

        enemySpawnedCount++;
    }
}
