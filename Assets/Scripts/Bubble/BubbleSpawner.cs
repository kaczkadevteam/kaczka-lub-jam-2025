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
            // Get the base spawn point
    
            // Spawn a bubble and reset the timer
            SpawnBubble();
            nextSpawn = 0;
            
        }
    }

    void SpawnBubble()
    {
        // Get random offset for the spawn point
        var bubbleSpawnOffsetX = Random.Range(-GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction, GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction);
        var bubbleSpawnOffsetZ = Random.Range(-GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction, GlobalConfig.Instance.blowerOrbitingRadius * GlobalConfig.Instance.bubbleSpawnOffsetFraction);

        // Generate a random spawn point
        var spawnPoint = new Vector3(baseSpawnPoint.position.x  + bubbleSpawnOffsetX, baseSpawnPoint.position.y, baseSpawnPoint.position.z  + bubbleSpawnOffsetZ);

        Debug.Log("Spawned a bubble @" + spawnPoint);
        // Spawn the bubble
        Instantiate(bubble, spawnPoint, Quaternion.identity, bubbleParent);

        // TODO: Assign the blower to the bubble
        // bubble.GetComponent<Bubble>().blower = blower;

        // Increment the enemy spawned count
        enemySpawnedCount++;
    }
}
