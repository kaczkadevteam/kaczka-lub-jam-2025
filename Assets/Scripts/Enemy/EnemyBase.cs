using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	[SerializeField] private EnemySO enemySO;
	private float currentHealth;
    private Transform playerLocation;
    
    void Start()
	{
		currentHealth = enemySO.health;
		playerLocation = GameObject.Find("Player").transform;
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
		transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, enemySO.moveSpeed * Time.deltaTime);
	}
	
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			SelfDestruct();
			GameManager.Instance.DecreaseHealth(enemySO);
		}

		if (collision.gameObject.tag == "Bubble") {
			SelfDestruct();
		}
	}
	
	public void SelfDestruct()
	{
		//todo: chance for droping upgrade for player to pick up
		Destroy(gameObject);
	}
	
	//todo
	private void DropUpgrade()
	{
	}
}
