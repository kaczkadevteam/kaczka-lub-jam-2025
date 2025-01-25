using System;
using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour
{
    [SerializeField]
    protected string upgradeName;

    public GameObject prefab;

    [SerializeField]
    protected float baseUpgradeValue;

    [SerializeField]
    private int level = 0;

    [SerializeField]
    private int maxLevel = 10;

    public int rarityRating = 1;

    public virtual void IncrementLevel()
    {
        if (CanUpgrade())
            level++;
    }

    public float GetUpgradeValue()
    {
        return baseUpgradeValue * level;
    }

    public int GetLevel()
    {
        return level;
    }

    public string GetUpgradeName()
    {
        return upgradeName;
    }

    public bool CanUpgrade()
    {
        return level < maxLevel;
    }

    public void SetMaxLevel(int maxLevel)
    {
        this.maxLevel = maxLevel;
    }
}
