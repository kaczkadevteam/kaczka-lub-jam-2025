using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlowerUpgrades
{
    [SerializeField]
    private float spawnRateInterval = 0.1f;
    public float SpawnRateInterval => spawnRateInterval;

    [SerializeField]
    private int bubbleSpawnerCount = 1;

    public int BubbleSpawnerCount {
        get => bubbleSpawnerCount; 
        set
        {
            for (; bubbleSpawnerCount < value; bubbleSpawnerCount++)
            {
                onBubbleSpawnerCountIncrement.ForEach(callback => callback());
            }

        }
    }
    public readonly List<Action> onBubbleSpawnerCountIncrement = new();
}
