using System.Collections.Generic;
using UnityEngine;

public class BlowerUpgradeManager : MonoBehaviour
{
    private static BlowerUpgradeManager _instance;

    public static BlowerUpgradeManager Instance => _instance;

    public BlowerRotationSpeedUpgrade blowerRotationSpeedUpgrade;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<UpgradeBase> GetManagerUpgrades()
    {
        return new List<UpgradeBase> { blowerRotationSpeedUpgrade };
    }
}
