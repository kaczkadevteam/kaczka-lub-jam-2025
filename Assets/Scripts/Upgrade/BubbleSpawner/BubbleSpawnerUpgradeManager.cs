using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class BubbleSpawnerUpgradeManager : MonoBehaviour
{
    private static BubbleSpawnerUpgradeManager _instance;

    public static BubbleSpawnerUpgradeManager Instance => _instance;

    [SerializeField]
    private GameObject blowerSpawnerPrefab;

    [SerializeField]
    private Transform baseBubbleSpawnerSpawnPoint;

    [SerializeField]
    private Transform bubbleParent;

    [SerializeField]
    private Transform blower;

    public BubbleSpawnerCountUpgrade bubbleSpawnerCountUpgrade;


    public BubbleSpawnerSpawnRateUpgrade bubbleSpawnerSpawnSpeedUpgrade;

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

        bubbleSpawnerCountUpgrade.onBubbleSpawnerCountIncrement.Add(SpawnBubbleSpawner);
    }

    private void SpawnBubbleSpawner()
    {
        switch (bubbleSpawnerCountUpgrade.GetLevel())
        {
            case 2:
                Spawn2Bubbles();
                break;
            case 3:
                Spawn3Bubbles();
                break;
            case 4:
                Spawn4Bubbles();
                break;
            case 5:
                Spawn5Bubbles();
                break;
            default:
                break;
        }
    }

    private void Spawn2Bubbles()
    {
        var spread = .5f;

        var bubbleLeftSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x - spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        var bubbleRightSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x + spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        SpawnBubbleList(new Vector3[] { bubbleLeftSpawnPoint, bubbleRightSpawnPoint });
    }

    // Spawn 3 bubbles in a triangle formation
    private void Spawn3Bubbles()
    {
        var spread = .75f;

        var bubbleLeftSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x - spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        var bubbleRightSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x + spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        var bubbleTopSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z + spread
        );

        SpawnBubbleList(
            new Vector3[] { bubbleLeftSpawnPoint, bubbleRightSpawnPoint, bubbleTopSpawnPoint }
        );
    }

    // Spawn 4 bubbles in a square formation
    private void Spawn4Bubbles()
    {
        var spread = 0.75f;

        var bubbleLeftSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x - spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        var bubbleRightSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x + spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        var bubbleTopSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z + spread
        );

        var bubbleBottomSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z - spread
        );

        SpawnBubbleList(
            new Vector3[]
            {
                bubbleLeftSpawnPoint,
                bubbleRightSpawnPoint,
                bubbleTopSpawnPoint,
                bubbleBottomSpawnPoint,
            }
        );
    }

    // Spawn 5 bubbles in a square formation with one in the center
    private void Spawn5Bubbles()
    {
        var spread = 1.25f;

        var bubbleLeftSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x - spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        var bubbleRightSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x + spread,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        var bubbleTopSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z + spread
        );

        var bubbleBottomSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z - spread
        );

        var bubbleMidSpawnPoint = new Vector3(
            baseBubbleSpawnerSpawnPoint.position.x,
            baseBubbleSpawnerSpawnPoint.position.y,
            baseBubbleSpawnerSpawnPoint.position.z
        );

        SpawnBubbleList(
            new Vector3[]
            {
                bubbleLeftSpawnPoint,
                bubbleRightSpawnPoint,
                bubbleTopSpawnPoint,
                bubbleBottomSpawnPoint,
                bubbleMidSpawnPoint,
            }
        );
    }

    private void SpawnBubbleList(Vector3[] spawnPoints)
    {
        ClearBubbleSpawners();

        foreach (var spawnPoint in spawnPoints)
        {
            // Spawn all bubbles under the same parent
            var bubbleSpawnerInstance = Instantiate(
                blowerSpawnerPrefab,
                spawnPoint,
                Quaternion.identity,
                baseBubbleSpawnerSpawnPoint
            );

            var bubblerSpawnerComponent = bubbleSpawnerInstance.GetComponent<BubbleSpawner>();
            bubblerSpawnerComponent.bubbleParent = bubbleParent;
            bubblerSpawnerComponent.blower = blower;
        }
    }

    //TODO: Temp, remove after upgrades pick up created
    [ContextMenu("Test Spawners Upgrades")]
    private void TestSpawnerUpgrade()
    {
        bubbleSpawnerCountUpgrade.IncrementLevel();
    }

    private void ClearBubbleSpawners()
    {
        foreach (Transform child in baseBubbleSpawnerSpawnPoint)
        {
            Destroy(child.gameObject);
        }
    }
}
