using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bacteria : EnemyBase
{
    private GameObject rootObject;
    // todo 
    private IEnumerator Replicate()
    {
        while (true)
        {
            yield return new WaitForSeconds(13f);
            Vector3 randomSpawnPointNearby = GetRandomSpawnPointNearby();
            EnemySpawnerManager.Instance.SpawnEnemy(rootObject.transform.position + randomSpawnPointNearby, enemySO);
        }
    }
    
    private Vector3 GetRandomSpawnPointNearby()
    {
        Vector3 randomSpawnPointNearby = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        return randomSpawnPointNearby;
    }

    new void Start()
    {
        base.Start();
        rootObject = transform.gameObject;
        StartCoroutine(Replicate());
    }
}
