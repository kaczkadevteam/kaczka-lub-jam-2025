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

    public void IncrementLevel()
    {
        level++;
    }

    public float GetUpgradeValue()
    {
        return baseUpgradeValue * level;
    }
}
