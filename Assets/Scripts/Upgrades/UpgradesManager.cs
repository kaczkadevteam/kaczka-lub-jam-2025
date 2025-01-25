using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    private static UpgradesManager _instance;

    public static UpgradesManager Instance => _instance;

    [Header("Spawner upgrades")]
    [SerializeField]
    private GameObject spawnerPrefab;

    [SerializeField]
    private Transform spawnerSpawnpoint;
    [SerializeField]
    private float spawnerSpawnOffset;

    [Header("Upgrades values")]
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
        var spawnerSpawnOffsetX = Random.Range(-spawnerSpawnOffset,spawnerSpawnOffset);
        var spawnerSpawnOffsetZ = Random.Range(-spawnerSpawnOffset, spawnerSpawnOffset);

        var spawnPoint = new Vector3(spawnerSpawnpoint.position.x + spawnerSpawnOffsetX, spawnerSpawnpoint.position.y, spawnerSpawnpoint.position.z + spawnerSpawnOffsetZ);

        Instantiate(spawnerPrefab, spawnPoint, Quaternion.identity);
    }

    //TODO: Temp, remove after upgrades pick up created
    [ContextMenu("Test Spawners Upgrades")]
    private void TestSpawnerUpgrade()
    {
        blowerUpgrades.BubbleSpawnerCount++;
    }
}
