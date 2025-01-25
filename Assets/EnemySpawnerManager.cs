using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using Random = UnityEngine.Random;


public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemySOList;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float spawnPointRadius = 10f;
    [Header("1 = 1 enemy/s 2 = 2 enemies/s etc...")]
    [SerializeField] private float spawnRate = 1f;

    [Serializable] 
    public class EnemyTuple
    {
        public EnemySO enemySO;
        public List<GameObject> enemyList;
    }
    public List<EnemyTuple> enemyList;
    public GameObject enemiesParent;
    public static EnemySpawnerManager Instance { get; private set; }
    
    public void SpawnEnemy(Vector3 spawnPoint, EnemySO enemySO = null)
    {
        EnemySO selectedEnemy = enemySO;
        if (enemySOList == null || enemySOList.Count == 0) return;
        if (selectedEnemy == null)
        {
            float totalWeight = 0f;
            foreach (EnemySO enemySOelement in enemySOList)
            {
                totalWeight += enemySOelement.spawnWeight;
            }

            float randomValue = Random.Range(0f, totalWeight);

            

            foreach (EnemySO enemySOelement in enemySOList)
            {
                randomValue -= enemySOelement.spawnWeight;
                if (randomValue <= 0f)
                {
                    selectedEnemy = enemySOelement;
                    break;
                }
            }

            if (selectedEnemy == null)
            {
                selectedEnemy = enemySOList[0];
            }
        }


        var enemyTuple = enemyList.Find(x => x.enemySO == selectedEnemy);
        
        GameObject gameObject = ReuseEnemy(enemyTuple?.enemyList);
        if(gameObject != null)
        {
            SetupEnemy(spawnPoint, gameObject);
        }
        
        GameObject enemyGo =  Instantiate(selectedEnemy.enemyPrefab, spawnPoint, Quaternion.identity);
        if(enemyTuple == null)
        {
            enemyList.Add(new EnemyTuple {enemySO = selectedEnemy, enemyList = new List<GameObject> {enemyGo}});
        }
        enemyGo.transform.SetParent(enemiesParent.transform);
        
        EnemyBase enemyBase = enemyGo.GetComponent<EnemyBase>();
        
        if (enemyBase != null)
        {
            enemyBase.SetPlayerLocation(playerLocation);
        }
        
        if (enemyTuple != null)
        {
            enemyTuple.enemyList.Add(enemyGo);
        }
    }


    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 randomPoint = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * spawnPointRadius;
        
        return randomPoint;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        StartCoroutine(ScaleEnemySpawnRate());
    }

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ScaleEnemySpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            spawnRate += GlobalConfig.Instance.enemySpawnRateGrowth;
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        Vector3 randomSpawnPoint = GetRandomSpawnPoint();
        SpawnEnemy(randomSpawnPoint);
        yield return new WaitForSeconds(1f/spawnRate);
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private GameObject ReuseEnemy(List<GameObject> enemyList)
    {
        foreach (GameObject enemy in enemyList)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        return null;
    }

    private void SetupEnemy(Vector3 spawnPoint, GameObject gameObject)
    {
        gameObject.transform.position = spawnPoint;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);
    }
}
