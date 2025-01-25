using UnityEngine;
using System.Collections.Generic;


public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemySOList;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float spawnPointRadius = 10f;
    [Header("1 = 1 enemy/s 2 = 2 enemies/s etc...")]
    [SerializeField] private float spawnRate = 1f;
    
    private void SpawnEnemy()
    {
        if (enemySOList == null || enemySOList.Count == 0) return;
        
        float totalWeight = 0f;
        foreach (EnemySO enemySO in enemySOList)
        {
            totalWeight += enemySO.spawnWeight;
        }
        
        float randomValue = Random.Range(0f, totalWeight);
        
        EnemySO selectedEnemy = null;
        
        foreach (EnemySO enemySO in enemySOList)
        {
            randomValue -= enemySO.spawnWeight;
            if (randomValue <= 0f)
            {
                selectedEnemy = enemySO;
                break;
            }
        }
        
        if (selectedEnemy == null)
        {
            selectedEnemy = enemySOList[0];
        }
        
        Vector3 randomSpawnPoint = GetRandomSpawnPoint();
        GameObject enemyGo = Instantiate(selectedEnemy.enemyPrefab, randomSpawnPoint, Quaternion.identity);
        
        EnemyBase enemyBase = enemyGo.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            enemyBase.SetPlayerLocation(playerLocation);
        }
    }


    private Vector3 GetRandomSpawnPoint()
    {
        
        Vector3 randomPoint = Random.insideUnitSphere.normalized * spawnPointRadius;
        randomPoint.y = 0;
        
        return randomPoint;
    }
    
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 1/spawnRate);
    }
}
