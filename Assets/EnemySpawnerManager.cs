using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawnerManager : MonoBehaviour
{
    public List<EnemyBase> enemyBaseList;
    public List<Transform> spawnPoints;

    public void Start()
    {
        Invoke("SpawnEnemy", 1f);   
    }
    
    void SpawnEnemy()
    {
        Instantiate(enemyBaseList.First(), spawnPoints.First().position, Quaternion.identity);
    }
}
