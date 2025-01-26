using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    public EnemySO enemySO;

    [SerializeField]
    public CapsuleCollider capsuleCollider;
    public Transform playerLocation;
    
	public void Start()
	{
		playerLocation = GameObject.Find("Player").transform;
		InitStats(enemySO);
        Invoke("SelfDestruct", 20f);
	}

	private void OnEnable()
	{
        playerLocation = GameObject.Find("Player").transform;
		transform.rotation = Quaternion.identity;
		InitStats(enemySO);
        Invoke("SelfDestruct", 20f);
	}

    void FixedUpdate()
    {
        EnemyMove();
    }

    public void SetPlayerLocation(Transform playerLocation)
    {
        this.playerLocation = playerLocation;
    }

    public void EnemyMove()
    {
        if (!playerLocation)
            return;
        if (transform.parent != EnemySpawnerManager.Instance.enemiesParent.transform)
            return;
        transform.position = Vector3.MoveTowards(
            transform.position,
            playerLocation.position,
            enemySO.moveSpeed * Time.deltaTime
        );
    }

    public void OnCollisionEnter(Collision collision)
    {
        //
        if (collision.gameObject.TryGetComponent<Bubble>(out var bubble))
        {
            bool wasEnemyCaptured = bubble.TryCaptureEnemy(this);

            Debug.Log($"wasEnemyCaptured: {wasEnemyCaptured}");

            if (wasEnemyCaptured)
                DropUpgrade();
        }

        if (
            transform.parent == EnemySpawnerManager.Instance.enemiesParent.transform
            && collision.gameObject.tag == "Player"
        )
        {
            SelfDestruct();
            GameManager.Instance.DecreaseHealth(enemySO);
        }
    }

    public void SelfDestruct()
    {
        //todo: chance for droping upgrade for player to pick up
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    private void DropUpgrade()
    {
        var f = UnityEngine.Random.Range(0, 100);

        var shouldDropUpgrade = f <= GlobalConfig.Instance.upgradeDropChance;

        if (shouldDropUpgrade)
            UpgradeLootManager.Instance.DispatchUpgrade(transform.position);
    }

    public void InitStats(EnemySO enemySO)
    {
        this.enemySO = enemySO;
        transform.localScale = new Vector3(enemySO.size, enemySO.size, enemySO.size);
    }
}
