using System.Collections.Generic;
using UnityEngine;

public class UpgradeLootManager : MonoBehaviour
{
    private static UpgradeLootManager _instance;

    public static UpgradeLootManager Instance => _instance;

    [SerializeField]
    private GameObject upgradeTabletParent;

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

    public List<UpgradeBase> GetAllUpgrades()
    {
        var bubbleUpgrades = BubbleUpgradeManager.Instance.GetManagerUpgrades();
        var bubbleSpawnerUpgrades = BubbleSpawnerUpgradeManager.Instance.GetManagerUpgrades();
        var blowerUpgrades = BlowerUpgradeManager.Instance.GetManagerUpgrades();

        var allUpgrades = new List<UpgradeBase>();

        allUpgrades.AddRange(bubbleUpgrades);
        allUpgrades.AddRange(bubbleSpawnerUpgrades);
        allUpgrades.AddRange(blowerUpgrades);

        return allUpgrades;
    }

    public List<UpgradeBase> GetPossibleToDropUpgrades()
    {
        var allUpgrades = GetAllUpgrades();
        var possibleToDropUpgrades = new List<UpgradeBase>();

        foreach (var upgrade in allUpgrades)
        {
            if (upgrade.CanUpgrade())
            {
                possibleToDropUpgrades.Add(upgrade);
            }
        }

        return possibleToDropUpgrades;
    }

    public Dictionary<UpgradeBase, int> GetPossibleUpgradeRarityWeights()
    {
        var allUpgrades = GetPossibleToDropUpgrades();
        var upgradeRarityWeights = new Dictionary<UpgradeBase, int>();
        var previousRarityWeight = 0;

        foreach (var upgrade in allUpgrades)
        {
            upgradeRarityWeights.Add(upgrade, previousRarityWeight + upgrade.rarityRating);
            previousRarityWeight += upgrade.rarityRating;
        }

        return upgradeRarityWeights;
    }

    public UpgradeBase GetRandomUpgrade()
    {
        var possibleToDropUpgrades = GetPossibleUpgradeRarityWeights();

        // Get the highest rarity weight from the last upgrade or 0 if there are no upgrades
        var highestRarityWeight = 0;

        // Hacky workaround to get the last rarity weight
        foreach (var upgrade in possibleToDropUpgrades)
        {
            highestRarityWeight = upgrade.Value;
        }

        var randomRarityWeight = Random.Range(0, highestRarityWeight);

        foreach (var upgrade in possibleToDropUpgrades)
        {
            if (randomRarityWeight <= upgrade.Value)
            {
                return upgrade.Key;
            }
        }

        return null;
    }

    public void DispatchUpgrade(Vector3 spawnPosition)
    {
        var upgrade = GetRandomUpgrade();

        if (upgrade != null)
        {
            upgrade.IncrementLevel();

            Instantiate(
                upgrade.prefab,
                spawnPosition,
                Quaternion.identity,
                upgradeTabletParent.transform
            );
        }
    }
}
