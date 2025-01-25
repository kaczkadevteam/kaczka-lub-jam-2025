using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	[SerializeField] public EnemySO enemySO;
    [SerializeField] public CapsuleCollider capsuleCollider;
	private float currentHealth;
    private Transform playerLocation;
    
	public void Start()
	{
		currentHealth = enemySO.health;
		playerLocation = GameObject.Find("Player").transform;
	}

	private void OnEnable()
	{
		transform.rotation = Quaternion.identity;
	}

	void Update()
	{
		EnemyMove();
	}
	
	public void SetPlayerLocation(Transform playerLocation)
	{
		this.playerLocation = playerLocation;
	}

	public void EnemyMove()
	{
		if(!playerLocation) return;
		if(transform.parent != EnemySpawnerManager.Instance.enemiesParent.transform) return;
		transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, enemySO.moveSpeed * Time.deltaTime);
	}
	
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			SelfDestruct();
			GameManager.Instance.DecreaseHealth(enemySO);
		}

		if (collision.gameObject.TryGetComponent<Bubble>(out var bubble)) {
			bubble.TryCaptureEnemy(this);
		}
	}

	public void SelfDestruct()
	{
		//todo: chance for droping upgrade for player to pick up
		gameObject.SetActive(false);
		// Destroy(gameObject);
	}
	
	//todo
	private void DropUpgrade()
	{
	}
}
