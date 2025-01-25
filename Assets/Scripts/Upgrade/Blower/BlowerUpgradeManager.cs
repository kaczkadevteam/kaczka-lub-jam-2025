using UnityEngine;

public class BlowerUpgradeManager : MonoBehaviour
{
    private static BlowerUpgradeManager _instance;

    public static BlowerUpgradeManager Instance => _instance;

    [SerializeField]
    private GameObject blowerSpawnerPrefab;

    [SerializeField]
    private Transform blowerSpawnerSpawnpoint;
    [SerializeField]
    private float blowerSpawnerSpawnOffset;

    [SerializeField]
    public BlowerUpgrades blowerUpgrades;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }

        blowerUpgrades.onBubbleSpawnerCountIncrement.Add(SpawnBubbleSpawner);
    }

    private void SpawnBubbleSpawner()
    {
        var spawnerSpawnOffsetX = Random.Range(-blowerSpawnerSpawnOffset,blowerSpawnerSpawnOffset);
        var spawnerSpawnOffsetZ = Random.Range(-blowerSpawnerSpawnOffset, blowerSpawnerSpawnOffset);

        var spawnPoint = new Vector3(blowerSpawnerSpawnpoint.position.x + spawnerSpawnOffsetX, blowerSpawnerSpawnpoint.position.y, blowerSpawnerSpawnpoint.position.z + spawnerSpawnOffsetZ);

        Instantiate(blowerSpawnerPrefab, spawnPoint, Quaternion.identity);
    }

    //TODO: Temp, remove after upgrades pick up created
    [ContextMenu("Test Spawners Upgrades")]
    private void TestSpawnerUpgrade()
    {
        blowerUpgrades.BubbleSpawnerCount++;
    }
}
