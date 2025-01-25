using System;
using System.Collections.Generic;

public class BubbleSpawnerCountUpgrade : UpgradeBase
{
    public int maxBubbleSpawnerCount = 5;

    public readonly List<Action> onBubbleSpawnerCountIncrement = new();

    private void Awake()
    {
        SetMaxLevel(maxBubbleSpawnerCount);
    }

    public int BubbleSpawnerCount => GetLevel();

    public override void IncrementLevel()
    {
        base.IncrementLevel();

        if (CanUpgrade())
            onBubbleSpawnerCountIncrement.ForEach(callback => callback());
    }
}
