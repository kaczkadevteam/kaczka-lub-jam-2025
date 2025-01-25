using UnityEngine;
using System.Collections.Generic;


public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<EnemyBase> enemyBaseList;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float spawnPointRadius = 10f;
    [Header("1 = 1 enemy/s 2 = 2 enemies/s etc...")]
    [SerializeField] private float spawnRate = 1f;
    
    public void SpawnEnemy()
    {
        Vector3 randomSpawnPoint = GetRandomSpawnPoint();
        EnemyBase enemyToSpawn = enemyBaseList[Random.Range(0, enemyBaseList.Count)];
        EnemyBase newEnemy = Instantiate(enemyToSpawn, randomSpawnPoint, Quaternion.identity);
        newEnemy.SetPlayerLocation(playerLocation);
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
