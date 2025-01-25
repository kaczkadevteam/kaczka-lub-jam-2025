using System;
using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour
{
    [SerializeField]
    protected string upgradeName;

    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    protected float baseUpgradeValue;

    [SerializeField]
    private int level = 0;

    [SerializeField]
    private int maxLevel = 10;

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

    public bool CanUpgrade()
    {
        return level <= maxLevel;
    }

    public void SetMaxLevel(int maxLevel)
    {
        this.maxLevel = maxLevel;
    }
}
