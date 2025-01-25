using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Virus : EnemyBase
{
    [SerializeField] private float chanceToMutate = 0.01f;
    
    private void TryMutate()
    {
        if(Random.value > chanceToMutate) return;
        int mutation = Random.Range(0, 4);
            
        switch (mutation)
        {
            case 0:
                enemySO.moveSpeed += 0.5f;
                break;
            case 1:
                enemySO.damageToPlayer += 1;
                break;
            case 2:
                enemySO.spawnWeight += 0.1f;
                break;
            case 3:
                enemySO.size *= 0.95f;
                break;
            default:
                break;
        }
        InitStats(enemySO);
    }
    
    new void Start()
    {
        base.Start();
        TryMutate();
    }
    
    void OnEnable()
    {
        TryMutate();
    }
    
    
}
