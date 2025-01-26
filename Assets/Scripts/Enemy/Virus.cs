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
                VirusEvolutionManager.Instance.moveSpeed += 0.5f;
                break;
            case 1:
                VirusEvolutionManager.Instance.damageToPlayer += 1;
                break;
            case 2:
                VirusEvolutionManager.Instance.spawnWeight += 0.1f;
                break;
            case 3:
                VirusEvolutionManager.Instance.size *= 0.95f;
                break;
            default:
                break;
        }
        InitStats();
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
    
    private void InitStats()
    {
        var scale = VirusEvolutionManager.Instance.size;
        transform.localScale = new Vector3(scale, scale, scale);
    }
    
    void FixedUpdate()
    {
        EnemyMove();
        base.EnemyRotate();
    }
    
    new private void EnemyMove()
    {
        if (!playerLocation)
            return;
        if (transform.parent != EnemySpawnerManager.Instance.enemiesParent.transform)
            return;
        var speed = VirusEvolutionManager.Instance.moveSpeed;
        transform.position = Vector3.MoveTowards(
            transform.position,
            playerLocation.position,
            speed * Time.deltaTime
        );
    }
    
}
