using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Virus : EnemyBase
{
    [SerializeField] private float chanceToMutate = 0.01f;
    
    private void Mutate()
    {
        if(Random.value > chanceToMutate) return;
        int mutation = Random.Range(0, 3);

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
                enemySO.size *= 10f;
                break;
            default:
                break;
        }
    }
    
    new void Start()
    {
        base.Start();
        
        if (Random.value < chanceToMutate)
        {
            Mutate();
        }
        
        StartCoroutine(TryToMutate());
    }

    private void OnEnable()
    {
        InitStats(enemySO);
    }

    public IEnumerator TryToMutate()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Mutate();
        }
    }
    
    
}
